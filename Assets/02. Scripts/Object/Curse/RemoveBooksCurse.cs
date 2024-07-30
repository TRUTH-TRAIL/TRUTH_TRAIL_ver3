using UnityEngine;

namespace TT
{
    public class RemoveBooksCurse : MonoBehaviour, ICurse
    {
        public string Description => "책장의 책을 치워봐";

        public LayerMask InteractionMask;
        public float InteractionRange = 3.0f;
        private Camera cam;
        private bool isJustOnce;

      
        private void Awake()
        {
            isJustOnce = false;
            cam = Camera.main;
            InteractionMask  = LayerMask.GetMask("Interactionable");
        }
        
        private void Update()
        {
            Collider collider = RaycastUtil.TryGetCollider(cam, InteractionRange, InteractionMask);
            if (collider != null)
            {
                IInteractable interactable = collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        InteractableObject o = interactable as Book;
                        if (o != null && o.InteractionType == InteractionType.Book && !isJustOnce)
                        {
                            isJustOnce = true;
                            Trigger();
                        }
                    }
                }
            }
        }
        
        public void Activate()
        {
            Player.Instance.CurrentCurse = Player.Instance.gameObject.AddComponent<RemoveBooksCurse>();
            Debug.Log("책장의 책을 치워봐 저주 발동!");
        }
        
        private void Trigger()
        {
            Debug.Log("저주가 발동되면 AI에게 풀리지 않는 어그로가 발동하여 사망에 이르게 된다");
        }
    }
}