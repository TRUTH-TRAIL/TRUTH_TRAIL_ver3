using UnityEngine;
using System.Collections;

namespace TT
{
    public class Drawer : InteractableObject
    {
        public enum SlideDirection
        {
            Left,
            Right,
            Forward,
            Backward
        }

        [Tooltip("이동 거리")] 
        public float SlideDistance = 1.0f;

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
            targetPosition = originalPosition + GetSlideVector(Direction) * SlideDistance;
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
                    return Vector3.forward;
            }
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

    }
}
