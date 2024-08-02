using UnityEngine;

namespace TT
{
    public class LookOutWindowCurse : MonoBehaviour, ICurse
    {
        public string Description => "창문 밖을 봐";
     
        private LayerMask WindowMask;
        private float InteractionRange = 2.0f;
        private Camera cam;

        private bool isJustOnce;
        
        private void Awake()
        {
            isJustOnce = false;
            cam = Camera.main;
            WindowMask = LayerMask.GetMask("Window");
        }

        private void Update()
        {
            Collider collider = RaycastUtil.TryGetCollider(cam, InteractionRange, WindowMask);
            bool isColliderNull = collider == null;
             
            if (!isColliderNull)
            {
                if (Vector3.Distance(transform.position, collider.gameObject.transform.position) < 3.0f && !isJustOnce)
                {
                    Trigger();
                    isJustOnce = true;
                }
            }
            
        }

        public void Activate()
        {
            // 저주 발동 로직
            var Player = FindObjectOfType<Player>();
            Player.CurrentCurse = Player.gameObject.AddComponent<LookOutWindowCurse>();
            Debug.Log("창문 밖을 봐 저주 발동!");
            // 추가적인 저주 효과 로직
        }
        
        private void Trigger()
        {
            var Player = FindObjectOfType<Player>();
            Player.IsDeadCurseState = true;
            Debug.Log("저주가 발동되면 AI에게 풀리지 않는 어그로가 발동하여 사망에 이르게 된다");
        }
    }
}