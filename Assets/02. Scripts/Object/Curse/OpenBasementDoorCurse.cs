using UnityEngine;

namespace TT
{
    public class OpenBasementDoorCurse : MonoBehaviour, ICurse
    {
        public string Description => "지하실 문을 열어봐";
        
        public void Activate()
        {
            var Player = FindObjectOfType<Player>();
            Player.CurrentCurse = Player.gameObject.AddComponent<OpenBasementDoorCurse>();
            Debug.Log("지하실 문을 열어봐 저주 발동!");
            // 추가적인 저주 효과 로직
        }
        
        private LayerMask BasementDoorMask;
        private float InteractionRange = 2.0f;
        private Camera cam;

        private void Awake()
        {
            cam = Camera.main;
            BasementDoorMask = LayerMask.GetMask("Interactionable");
        }

        private void Update()
        {
            Collider collider = RaycastUtil.TryGetCollider(cam, InteractionRange, BasementDoorMask);
            if (Input.GetMouseButtonDown(0))
            {
                if (FindObjectOfType<BaseManager>().State is not GameState.MainGame) return;
                
                bool isColliderNull = collider == null;
                   
                if (!isColliderNull && collider.gameObject.GetComponent<Door>().DoorType == DoorType.Basement)
                {
                    Trigger();
                }
            }
        }
        
        private void Trigger()
        {
            FindObjectOfType<Player>().IsDeadCurseState = true;
            Debug.Log("저주가 발동되면 AI에게 풀리지 않는 어그로가 발동하여 사망에 이르게 된다");

            GameManager.Instance.CurseGameOver();
        }
    }
}