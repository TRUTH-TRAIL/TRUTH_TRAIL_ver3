using UnityEngine;
using System.Collections;

namespace TT
{
    public class Book : InteractableObject
    {
        public enum SlideDirection
        {
            Left,
            Right,
            Forward,
            Backward
        }

        [Tooltip("밀리는 거리")] 
        public float SlideDistance = 0.5f;

        [Tooltip("밀리는 방향")] 
        public SlideDirection Direction = SlideDirection.Right;

        [Tooltip("이동 시간")] 
        public float SlideDuration = 1.0f; 

        private Vector3 originalPosition;
        private bool hasInteracted = false;

        private void Start()
        {
            originalPosition = transform.localPosition;
            OnInteractionEvent.AddListener(Slide);

            gameObject.AddComponent<MeshCollider>();
        }

        private void OnDestroy()
        {
            OnInteractionEvent.RemoveListener(Slide);
        }

        private void Slide()
        {
            if (hasInteracted) return;
            hasInteracted = true;

            Vector3 slideVector = GetSlideVector(Direction);
            Vector3 targetPosition = originalPosition + slideVector * SlideDistance;
            StartCoroutine(SlideCoroutine(targetPosition));
        }

        private Vector3 GetSlideVector(SlideDirection direction)
        {
            switch (direction)
            {
                case SlideDirection.Left:
                    return Vector3.left;
                case SlideDirection.Right:
                    return Vector3.right;
                case SlideDirection.Forward:
                    return Vector3.forward;
                case SlideDirection.Backward:
                    return Vector3.back;
                default:
                    return Vector3.right;
            }
        }

        private IEnumerator SlideCoroutine(Vector3 targetPosition)
        {
            float elapsedTime = 0.0f;
            Vector3 startingPosition = transform.localPosition;

            while (elapsedTime < SlideDuration)
            {
                transform.localPosition = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / SlideDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.localPosition = targetPosition;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
            {
                Vector3 slideVector = GetSlideVector(Direction);
                Vector3 targetPosition = transform.position + slideVector * SlideDistance;

                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, targetPosition);
                Gizmos.DrawSphere(targetPosition, 0.05f);
            }
        }
#endif
    }
}
