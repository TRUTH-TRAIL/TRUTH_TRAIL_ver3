using UnityEngine;
using System.Collections;

namespace TT
{
    public class Frame : InteractableObject
    {
        public float ShakeDuration = 0.5f; 
        public float ShakeAmount = 0.01f; 
        private Vector3 originalPosition;

        protected override void Awake()
        {
            base.Awake();
            originalPosition = transform.localPosition;
            OnInteractionEvent.AddListener(Shake);

            gameObject.AddComponent<MeshCollider>();
        }

        private void OnDestroy()
        {
            OnInteractionEvent.RemoveListener(Shake);
        }

        private void Shake()
        {
            StartCoroutine(ShakeCoroutine());
        }

        private IEnumerator ShakeCoroutine()
        {
            float shakeTimeRemaining = ShakeDuration;
            while (shakeTimeRemaining > 0)
            {
                Vector3 shakePosition = originalPosition + Random.insideUnitSphere * ShakeAmount;
                shakePosition.x = originalPosition.x;
                transform.localPosition = shakePosition;
                shakeTimeRemaining -= Time.deltaTime;
                yield return null;
            }
            transform.localPosition = originalPosition;
        }
    }
}