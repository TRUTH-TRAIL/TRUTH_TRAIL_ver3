﻿// Perfect Culling (C) 2022 Patrick König
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace Koenigz.PerfectCulling
{
    public class PerfectCullingBakerNativeVulkanWin64 : PerfectCullingBaker
    {
        public override string RendererName => "Native (Vulkan)";

        public override int BatchCount => 4096 * 2;
        
        private readonly PerfectCullingSceneColor m_sceneColor;

        private List<UnityEngine.Object> m_disposeList = new List<Object>();
        
        public PerfectCullingBakerNativeVulkanWin64(PerfectCullingBakeSettings perfectCullingBakeSettings) 
            : base(perfectCullingBakeSettings)
        {
            m_sceneColor = new PerfectCullingSceneColor(perfectCullingBakeSettings.Groups, perfectCullingBakeSettings.AdditionalOccluders, false);
            
            List<Renderer> allRenderers = new List<Renderer>();
            Dictionary<Renderer, int> groupMapping = new Dictionary<Renderer, int>();

            const int AdditionalOccluderGroup = -1;
            
            for (int indexGroup = 0; indexGroup < m_bakeSettings.Groups.Length; ++indexGroup)
            {
                foreach (Renderer renderer in m_bakeSettings.Groups[indexGroup].renderers)
                {
                    if (groupMapping.TryGetValue(renderer, out int existingRendererGroup))
                    {
                        if (indexGroup != existingRendererGroup)
                        {
                            PerfectCullingLogger.LogError(
                                $"Duplicated renderer {renderer.name} (group {indexGroup}, current {existingRendererGroup}), ignoring");
                        }
                        
                        continue;
                    }
                    
                    allRenderers.Add(renderer);
                    groupMapping.Add(renderer, indexGroup);
                }
            }

            if (m_bakeSettings.AdditionalOccluders != null)
            {
                allRenderers.AddRange(m_bakeSettings.AdditionalOccluders);

                foreach (Renderer r in m_bakeSettings.AdditionalOccluders)
                {
                    if (groupMapping.ContainsKey(r))
                    {
                        // Already part of the bake
                        continue;
                    }
                    
                    groupMapping.Add(r, AdditionalOccluderGroup);
                }
            }

            // Remove other LOD levels
            {
                LODGroup[] allLODs = Object.FindObjectsOfType<LODGroup>();

                HashSet<Renderer> allRenderersHashSet = new HashSet<Renderer>(allRenderers);
                HashSet<Renderer> lodKeepHashSet = new HashSet<Renderer>();
                HashSet<Renderer> lodOtherHashSet = new HashSet<Renderer>();

                foreach (LODGroup lodGroup in allLODs)
                {
                    LOD[] lods = lodGroup.GetLODs();
                    
                    // We simply keep the first LOD that contains Renderers
                    int lodToKeep = 0;

                    for (int i = 0; i < lods.Length; ++i)
                    {
                        if (lods[i].renderers.Length != 0)
                        {
                            lodToKeep = i;
                            
                            break;
                        }
                    }

                    for (int i = 0; i < lods.Length; ++i)
                    {
                        foreach (Renderer r in lods[i].renderers)
                        {
                            if (i == lodToKeep)
                            {
                                lodKeepHashSet.Add(r);
                            }
                            else
                            {
                                lodOtherHashSet.Add(r);
                            }
                        }
                    }
                }

                foreach (Renderer r in lodOtherHashSet)
                {
                    if (lodKeepHashSet.Contains(r))
                    {
                        continue;
                    }

                    if (lodOtherHashSet.Contains(r) && allRenderersHashSet.Contains(r))
                    {
                        allRenderers.Remove(r);
                    }
                }
            }

            Vector3[] samplingLocations = perfectCullingBakeSettings.SamplingLocations.Where(x => x.Active).Select(x => x.Position).ToArray();

            for (int i = 0; i < samplingLocations.Length; ++i)
            {
                var newBakeHandle = new PerfectCullingBakerNativeVulkanWin64Handle()
                {
                    m_hash = m_sceneColor.Hash
                };
            
                handles.Add(newBakeHandle);
            }

            var nativeMeshRenderers = PrepareNativeMeshRendererData(allRenderers, groupMapping);

            UnityEngine.GameObject[] rs = Object.FindObjectsOfType<UnityEngine.GameObject>();

            foreach (var r in rs)
            {
                r.SetActive(false);
            }
            
            System.GC.Collect();
            Resources.UnloadUnusedAssets();
            
#if UNITY_EDITOR
            UnityEditor.Selection.activeGameObject = null;
            UnityEditor.SceneView.RepaintAll();
#endif

            pc_renderer_vulkan.Render(new pc_renderer_vulkan.pc_renderer_settings()
            {
                SamplingPositions = samplingLocations,
                NativeMeshRenderers = nativeMeshRenderers,
                baker = this
            });
        }

        public PerfectCullingBakerNativeVulkanWin64Handle GetHandleAt(int index) => handles[index];
        

        private List<PerfectCullingBakerNativeVulkanWin64Handle> handles = new List<PerfectCullingBakerNativeVulkanWin64Handle>();

        private int currentIndex = 0;
        
        public override PerfectCullingBakerHandle SamplePosition(Vector3 pos)
        {
            var tmp = handles[currentIndex];

            ++currentIndex;

            return tmp;
        }

        private pc_renderer_vulkan.NativeMeshRenderers[] PrepareNativeMeshRendererData(List<Renderer> inputRenderers, Dictionary<Renderer, int> groupMappingDict)
        {
            const int AdditionalOccluderGroup = -1;
            
            List<pc_renderer_vulkan.NativeMeshRenderers> finalResult =
                new List<pc_renderer_vulkan.NativeMeshRenderers>();
            
            var groupedRenderers = inputRenderers.GroupBy(u =>
                {
                    MeshFilter mf = u.GetComponent<MeshFilter>();

                    // Take into account scaling because Unity mirrors the mesh, etc. based on that.
                    Vector3Int scaleSign = new Vector3Int(
                        (int)Mathf.Sign(u.transform.lossyScale.x),
                        (int)Mathf.Sign(u.transform.lossyScale.y),
                        (int)Mathf.Sign(u.transform.lossyScale.z));
                    
                    Mesh sharedMesh = (mf == null ? null : mf.sharedMesh);
                    
                    return new { sharedMesh, scaleSign };
                });
            
            foreach (var rendererGroup in groupedRenderers)
            {
                if (rendererGroup.Key == null)
                {
                    continue;
                }
                
                Renderer firstRenderer = rendererGroup.First();

                MeshFilter mf = firstRenderer.GetComponent<MeshFilter>();

                if (mf == null)
                {
                    continue;
                }
                
                // We can split it apart once and just use it for all instances
                Mesh thisMesh = mf.sharedMesh;

                if (thisMesh == null)
                {
                    continue;
                }

                List<Mesh> subMeshes = new List<Mesh>();
                List<GameObject> debugParents = new List<GameObject>();

                for (int indexMaterial = 0; indexMaterial < thisMesh.subMeshCount; ++indexMaterial)
                {
                    int[] oldIndices = thisMesh.GetTriangles(indexMaterial);
                    List<Vector3> oldVerts = new List<Vector3>();
                    thisMesh.GetVertices(oldVerts);

                    int[] newIndices = new int[oldIndices.Length];
                    List<Vector3> newVerts = new List<Vector3>();

                    Dictionary<int, int> map = new Dictionary<int, int>();
                    for (int i = 0; i < oldIndices.Length; i++)
                    {
                        int oldIndex = oldIndices[i];

                        if (!map.TryGetValue(oldIndex, out var newIndex))
                        {
                            newIndex = newVerts.Count;

                            newVerts.Add(Vector3.Scale(oldVerts[oldIndex], rendererGroup.Key.scaleSign));

                            map.Add(oldIndex, newIndex);
                        }

                        newIndices[i] = newIndex;
                    }

                    for (int i = 0; i < newIndices.Length; i += 3)
                    {
                        if (rendererGroup.Key.scaleSign.x == -1)
                        {
                            (newIndices[i], newIndices[i + 2]) = (newIndices[i + 2], newIndices[i]);
                        }
                        if (rendererGroup.Key.scaleSign.y == -1)
                        {
                            (newIndices[i], newIndices[i + 1]) = (newIndices[i + 1], newIndices[i]);
                        }
                        if (rendererGroup.Key.scaleSign.z == -1)
                        {
                            (newIndices[i + 1], newIndices[i + 2]) = (newIndices[i + 2], newIndices[i + 1]);
                        }
                    }

                    Mesh m = new Mesh();
                    m.indexFormat = IndexFormat.UInt32;
                    m.vertices = newVerts.ToArray();
                    m.triangles = newIndices;

                    subMeshes.Add(m);
                    
                    debugParents.Add(new GameObject("DEBUG PARENT " + thisMesh.name + "  " + rendererGroup.Key.scaleSign + " " + indexMaterial.ToString()));
                    
                    Vector3[] verts = m.vertices;
                    int[] indices = m.GetIndices(0);
                    
                    var nativeMeshData = new pc_renderer_vulkan.NativeMeshData()
                    {
                        vertCount = verts.Length,
                        verts = verts,
                    
                        indCount = indices.Length,
                        indices = indices,
                    };

                    List<pc_renderer_vulkan.NativeRendererTransformation> transformations =
                        new List<pc_renderer_vulkan.NativeRendererTransformation>();
                    
                    foreach (Renderer renderer in rendererGroup)
                    {
                        Material[] mats = renderer.sharedMaterials;

                        // Might have more materials or sub-meshes. Need to cap it to the lowest number.
                        int maxMaterialCount = Mathf.Min(mats.Length, thisMesh.subMeshCount);

                        if (mats.Length != thisMesh.subMeshCount)
                        {
                            PerfectCullingLogger.LogWarning(
                                $"Number of materials ({mats.Length}) doesn't match number of sub-meshes ({thisMesh.subMeshCount}) for {renderer.name}.",
                                renderer.gameObject);
                        }

                        if (indexMaterial >= maxMaterialCount)
                        {
                            continue;
                        }
                        
                        bool isTransparent = PerfectCullingEditorUtil.IsMaterialTransparent(mats[indexMaterial]);

                        Color32 actualColor = PerfectCullingConstants.ClearColor;
                        
                        if (groupMappingDict[renderer] != AdditionalOccluderGroup)
                        {
                            actualColor = m_sceneColor.Colors[groupMappingDict[renderer]];
                        }
                        actualColor.a = (isTransparent ? PerfectCullingEditorUtil.TRANSPARENT_RENDER_COLOR : PerfectCullingEditorUtil.OPAQUE_RENDER_COLOR);

                        PerfectCullingRendererTag tag = renderer.GetComponent<PerfectCullingRendererTag>();

                        if (tag != null)
                        {
                            switch (tag.ForcedBakeRenderMode)
                            {
                                case EBakeRenderMode.Opaque:
                                    actualColor.a = PerfectCullingEditorUtil.OPAQUE_RENDER_COLOR;
                                    break;
                                
                                case EBakeRenderMode.Transparent:
                                    actualColor.a = PerfectCullingEditorUtil.TRANSPARENT_RENDER_COLOR;
                                    break;
                            }
                        }
                    
                        GameObject go = new GameObject(thisMesh.name + " " + indexMaterial.ToString());
                        go.transform.SetPositionAndRotation(renderer.transform.position, renderer.transform.rotation);
                        
                        go.transform.localScale = new Vector3(
                            Mathf.Abs(renderer.transform.lossyScale.x),
                            Mathf.Abs(renderer.transform.lossyScale.y), 
                            Mathf.Abs(renderer.transform.lossyScale.z));
                        
                        go.AddComponent<MeshFilter>().sharedMesh = subMeshes[indexMaterial];
                        
                        MeshRenderer mr = go.AddComponent<MeshRenderer>();
                        mr.sharedMaterial = mats[indexMaterial];

                        pc_renderer_vulkan.NativeRendererTransformation t = new pc_renderer_vulkan.NativeRendererTransformation()
                        {
                            boundsCenter = mr.bounds.center,
                            boundsSize = mr.bounds.size,
                    
                            mat4x4 = go.transform.localToWorldMatrix,
                
                            color = (Color)actualColor,
                        };
                        
                        go.transform.parent = debugParents[indexMaterial].transform;
                        
                        m_disposeList.Add(go);
                        
                        transformations.Add(t);
                    }
                    
                    var nativeMeshRenderers = new pc_renderer_vulkan.NativeMeshRenderers()
                    {
                        meshData = nativeMeshData,
                    
                        transformations = transformations.ToArray(),
                        transformationCount = transformations.Count,
                    };
                    
                    finalResult.Add(nativeMeshRenderers);
                }
            }

            return finalResult.ToArray();
        }

        public override void Dispose()
        {
            System.Threading.Thread.Sleep(5000);
            
            m_sceneColor.Dispose();
            
            foreach (UnityEngine.Object obj in m_disposeList)
            {
                Object.DestroyImmediate(obj);
            }
            
            m_disposeList.Clear();
            m_disposeList = null;
            
            pc_renderer_vulkan.JoinThread();
        }
    }
}