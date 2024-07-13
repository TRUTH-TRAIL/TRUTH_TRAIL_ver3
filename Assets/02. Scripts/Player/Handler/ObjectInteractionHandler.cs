using UnityEngine;

namespace TT
{
    public class ObjectInteractionHandler : MonoBehaviour
    {    
        public float InteractionRange = 2.0f;
        
        private Camera cam;

        private void Awake()
        {
            cam = Camera.main;
        }
        
        private void Update()
        {
            Collider collider = RaycastUtil.TryGetPickupableCollider(cam, InteractionRange);
            if (collider != null)
            {
                IInteractable interactable = collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        interactable.Interact();
                    }
                }
            }
        }
    }
}
