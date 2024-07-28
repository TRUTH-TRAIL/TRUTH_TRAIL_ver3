using UnityEngine;
using System.Collections;

namespace TT
{
    public class Drawer : InteractableObject
    {
        [Tooltip("이동 거리")] 
        public float SlideDistance;

        [Tooltip("이동 방향")] 
        public SlideDirection Direction = SlideDirection.Forward;

        [Tooltip("이동 시간")] 
        public float SlideDuration = 1.0f; 

        private Vector3 originalPosition;
        private Vector3 targetPosition;
        private bool isOpen = false;

        private void Start()
        {
            originalPosition = transform.localPosition;
            targetPosition = originalPosition + MovementUtils.GetSlideVector(transform, Direction) * SlideDistance;
            OnInteractionEvent.AddListener(ToggleDrawer);

            gameObject.AddComponent<MeshCollider>();
        }

        private void OnDestroy()
        {
            OnInteractionEvent.RemoveListener(ToggleDrawer);
        }

        private void ToggleDrawer()
        {
            if (isOpen)
            {
                StartCoroutine(SlideCoroutine(originalPosition));
            }
            else
            {
                StartCoroutine(SlideCoroutine(targetPosition));
            }
            isOpen = !isOpen;
        }
        

        private IEnumerator SlideCoroutine(Vector3 targetPos)
        {
            float elapsedTime = 0.0f;
            Vector3 startingPosition = transform.localPosition;

            while (elapsedTime < SlideDuration)
            {
                transform.localPosition = Vector3.Lerp(startingPosition, targetPos, elapsedTime / SlideDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.localPosition = targetPos;
        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
            {
                Vector3 slideVector = MovementUtils.GetSlideVector(transform, Direction);
                Vector3 targetPosition = transform.position + slideVector * SlideDistance;

                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position + Vector3.up*0.1f, targetPosition+ Vector3.up*0.1f);
                Gizmos.DrawSphere(targetPosition+ Vector3.up*0.1f, 0.025f);
            }
        }
#endif
    }
}
