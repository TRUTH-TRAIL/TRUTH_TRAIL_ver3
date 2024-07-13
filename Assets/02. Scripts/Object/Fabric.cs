using UnityEngine;
using System.Collections;

namespace TT
{
    public class Fabric : InteractableObject
    {
        [Tooltip("올라가는 주기")] public float RiseDuration = 2.0f; 
        [Tooltip("올라가는 높이")] public float RiseHeight = 2.0f;  
        private Vector3 originalPosition;
        private Material curtainMaterial;
        private Color originalColor;
        private LODGroup lodGroup;

        private MeshRenderer renderer;
        
        private void Awake()
        {
            originalPosition = transform.localPosition;
            OnInteractionEvent.AddListener(RiseAndFade);

            renderer = GetComponent<MeshRenderer>();
            gameObject.AddComponent<MeshCollider>();
            
            curtainMaterial = renderer.material;
            originalColor = curtainMaterial.color;
        }

        private void OnDestroy()
        {
            OnInteractionEvent.RemoveListener(RiseAndFade);
        }

        private void RiseAndFade()
        {
            if (curtainMaterial != null)
            {
                StartCoroutine(RiseAndFadeCoroutine());
            }
            else
            {
                Debug.LogError("Active LOD renderer's material is not set.");
            }
        }

        private IEnumerator RiseAndFadeCoroutine()
        {
            float elapsedTime = 0.0f;
            Vector3 targetPosition = originalPosition + new Vector3(0, RiseHeight, 0);

            while (elapsedTime < RiseDuration)
            {
                transform.localPosition = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / RiseDuration);

                Color newColor = originalColor;
                newColor.a = Mathf.Lerp(1, 0, elapsedTime / RiseDuration);
                curtainMaterial.color = newColor;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.localPosition = targetPosition;
            Color finalColor = originalColor;
            finalColor.a = 0;
            curtainMaterial.color = finalColor;
            
            lodGroup = GetComponentInParent<LODGroup>();
            if (lodGroup != null)
            {
                lodGroup.gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
