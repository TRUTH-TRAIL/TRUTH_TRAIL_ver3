using UnityEngine;

namespace TT
{
    public abstract class BaseController<T> : MonoBehaviour where T : class
    {
        public LayerMask BaseLayerMask;
        public float BaseRange;

        public string BaseString;
        private Camera cam;

        protected virtual void Awake()
        {
            cam = Camera.main;
        }

        private void Update()
        {
            Collider collider = RaycastUtil.TryGetPickupableCollider(cam, BaseRange, BaseLayerMask);
            if (collider != null)
            {
                T actionable = collider.GetComponent<T>();
                if (actionable != null)
                {
                    HandleAction(actionable);
                }
                else
                {
                    InteractionTextUI.Instance.SetPickupTextActive(false, BaseString);
                }
            }
            else
            {
                InteractionTextUI.Instance.SetPickupTextActive(false, BaseString);
            }
        }
        
        protected abstract void HandleAction(T actionable);
    }
}
