using UnityEngine;

namespace TT
{
    public class ExitHouseCurse : MonoBehaviour, ICurse
    {
        public string Description => "집 밖으로 나가봐";

        public void Activate()
        {
            // 저주 발동 로직
            var o = FindObjectOfType<Player>();
            o.CurrentCurse = o.gameObject.AddComponent<ExitHouseCurse>();
            Debug.Log("집 밖으로 나가봐 저주 발동!");
        }

        private LayerMask EntryFoyerDoorMask;
        private float InteractionRange = 2.0f;
        private Camera cam;

        private void Awake()
        {
            cam = Camera.main;
            EntryFoyerDoorMask = LayerMask.GetMask("Interactionable");
        }

        private void Update()
        {
            Collider collider = RaycastUtil.TryGetCollider(cam, InteractionRange, EntryFoyerDoorMask);
            if (Input.GetMouseButtonDown(0))
            {
                if (FindObjectOfType<BaseManager>().State is not GameState.MainGame) return;
                
                bool isColliderNull = collider == null;
             
                if (!isColliderNull && collider.gameObject.GetComponent<Door>().DoorType == DoorType.EntryFoyer)
                {
                    Trigger();
                }
            }
        }

        private void Trigger()
        {
            var Player = FindObjectOfType<Player>();
            Player.IsDeadCurseState = true;
            Debug.Log("저주가 발동되면 AI에게 풀리지 않는 어그로가 발동하여 사망에 이르게 된다");

            GameManager.Instance.CurseGameOver();
        }
    }
}