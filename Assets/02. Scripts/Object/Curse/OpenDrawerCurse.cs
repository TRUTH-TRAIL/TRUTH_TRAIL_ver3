using UnityEngine;

namespace TT
{
    public class OpenDrawerCurse : MonoBehaviour, ICurse
    {
        public string Description => "서랍을 열어봐";
        
        public void Activate()
        {
            // 저주 발동 로직
            var Player = FindObjectOfType<Player>();
            Player.CurrentCurse = Player.gameObject.AddComponent<OpenDrawerCurse>();
            Debug.Log("서랍을 열어봐 저주 발동!");
        }
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
                        Drawer drawer = interactable as Drawer;
                        if (drawer != null && drawer.InteractionType == InteractionType.Drawer && !isJustOnce)
                        {
                            isJustOnce = true;
                            Trigger();
                        }
                    }
                }
            }
        }
        private void Trigger()
        {
            
            FindObjectOfType<Player>().IsDeadCurseState = true;
            Debug.Log("저주가 발동되면 AI에게 풀리지 않는 어그로가 발동하여 사망에 이르게 된다");
        }
    }
}