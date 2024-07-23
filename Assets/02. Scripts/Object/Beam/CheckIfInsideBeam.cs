using UnityEngine;

namespace TT
{
    [RequireComponent(typeof(Collider))]
    public class CheckIfInsideBeam : MonoBehaviour
    {
        bool m_IsInsideBeam = false;
        [SerializeField] private Collider m_Collider = null;

        [SerializeField] private MeshRenderer meshRenderer;
        
        private void Start()
        {
            if (m_Collider == null)
            {
                m_Collider = GetComponent<Collider>();
            }

            if (meshRenderer == null)
            {
                meshRenderer = GetComponent<MeshRenderer>();
            }
        }

        private void FixedUpdate()
        {
            m_IsInsideBeam = false;
        }

        private void OnTriggerStay(Collider trigger)
        {
            var dynamicOcclusion = trigger.GetComponent<VLB.DynamicOcclusionRaycasting>();

            if (dynamicOcclusion)
            {
                m_IsInsideBeam = !dynamicOcclusion.IsColliderHiddenByDynamicOccluder(m_Collider);
            }
            else
            {
                m_IsInsideBeam = true;
            }
        }

        private void Update()
        {
            meshRenderer.enabled = m_IsInsideBeam;
        }
    }

}
