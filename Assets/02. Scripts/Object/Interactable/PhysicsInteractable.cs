using UnityEngine;

namespace TT
{
    [RequireComponent(typeof(Rigidbody))]
    public class PhysicsInteractable : InteractableObject
    {
        [Tooltip("힘의 세기")]
        public float forceStrength = 7f;

        [Tooltip("회전력의 세기")]
        public float torqueStrength = 5f;

        private Rigidbody rb;
        private Camera cam;
        
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            OnInteractionEvent.AddListener(ApplyPhysics);
            cam = Camera.main;
        }

        private void OnDestroy()
        {
            OnInteractionEvent.RemoveListener(ApplyPhysics);
        }

        private void ApplyPhysics()
        {
            // 플레이어의 방향을 기반으로 힘의 방향 설정
            Vector3 forceDirection = cam.transform.forward;
            Vector3 force = forceDirection * forceStrength;
            Vector3 torque = Random.insideUnitSphere * torqueStrength;

            // 힘과 회전력 적용
            rb.AddForce(force, ForceMode.Impulse);
            rb.AddTorque(torque, ForceMode.Impulse);

            MainGameSoundManager.Instance.PlaySFX("Click_1");
        }
    }
}