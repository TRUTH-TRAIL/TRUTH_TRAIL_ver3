using UnityEngine;

namespace TT
{
    public abstract class BaseController<T> : MonoBehaviour where T : class
    {
        public LayerMask BaseLayerMask;
        public float BaseRange;
        public string BaseString;

        private Camera cam;
        private bool isTextVisible = false;

        protected virtual void Awake()
        {
            cam = Camera.main;
        }

        private void Update()
        {
            Collider collider = RaycastUtil.TryGetCollider(cam, BaseRange, BaseLayerMask);
            if (collider != null)
            {
                T actionable = collider.GetComponent<T>();
                if (actionable != null)
                {
                    if (!isTextVisible)
                    {
                        InteractionTextUI.Instance.SetPickupTextActive(true, BaseString);
                        isTextVisible = true;
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        HandleAction(actionable);
                    }
                }
                else
                {
                    if (isTextVisible)
                    {
                        InteractionTextUI.Instance.SetPickupTextActive(false, BaseString);
                        isTextVisible = false;
                    }
                }
            }
            else
            {
                if (isTextVisible)
                {
                    InteractionTextUI.Instance.SetPickupTextActive(false, BaseString);
                    isTextVisible = false;
                }
            }
        }

        protected abstract void HandleAction(T actionable);
    }
}
