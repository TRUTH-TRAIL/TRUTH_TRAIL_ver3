using UnityEngine;
using System.Collections;

namespace TT
{
    public class BookcaseDoor : InteractableObject
    {
        [Tooltip("열리는 각도")] 
        public float OpenAngle = 90.0f;

        [Tooltip("회전 속도")] 
        public float RotationDuration = 1.0f; 

        [Tooltip("회전 피벗")] 
        public Transform Pivot; 

        private Quaternion closedRotation;
        private Quaternion openRotation;
        private bool isOpen = false;
        private bool isRotating = false;

        private void Start()
        {
            if (Pivot == null)
            {
                Pivot = transform; // 피벗이 설정되지 않은 경우 본인을 피벗으로 설정
            }

            closedRotation = Pivot.localRotation;
            openRotation = closedRotation * Quaternion.Euler(0, OpenAngle, 0);
            OnInteractionEvent.AddListener(ToggleDoor);

            gameObject.AddComponent<MeshCollider>();
        }

        private void OnDestroy()
        {
            OnInteractionEvent.RemoveListener(ToggleDoor);
        }

        private void ToggleDoor()
        {
            if (!isRotating)
            {
                StartCoroutine(RotateDoor(isOpen ? closedRotation : openRotation));
                if(isOpen) MainGameSoundManager.Instance.PlaySFX("SFX_GlasscaseOpen");
                else MainGameSoundManager.Instance.PlaySFX("SFX_GlasscaseClose");
                isOpen = !isOpen;
            }
        }

        private IEnumerator RotateDoor(Quaternion targetRotation)
        {
            isRotating = true;
            float elapsedTime = 0.0f;
            Quaternion startingRotation = Pivot.localRotation;

            while (elapsedTime < RotationDuration)
            {
                Pivot.localRotation = Quaternion.Slerp(startingRotation, targetRotation, elapsedTime / RotationDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            Pivot.localRotation = targetRotation;
            isRotating = false;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
            {
                Gizmos.color = Color.red;
                if (Pivot != null)
                {
                    Vector3 direction = (openRotation * Vector3.forward).normalized;
                    Gizmos.DrawRay(Pivot.position, direction);
                }
                else
                {
                    Vector3 direction = (Quaternion.Euler(0, OpenAngle, 0) * Vector3.forward).normalized;
                    Gizmos.DrawRay(transform.position, direction);
                }
            }
        }
#endif
    }
}
