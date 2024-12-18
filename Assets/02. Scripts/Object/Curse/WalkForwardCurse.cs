using UnityEngine;

namespace TT
{
    public class WalkForwardCurse : MonoBehaviour, ICurse
    {
        public string Description => "너 앞으로 되게 잘 걷는다. 계속 앞으로 걸어봐";

        private bool isJustOnce;
        
        private void Awake()
        {
            isJustOnce = false;
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W) && !isJustOnce)
            {
                Trigger();
                isJustOnce = true;
            }
        }

        public void Activate()
        {
            var Player = FindObjectOfType<Player>();
            Player.CurrentCurse = Player.gameObject.AddComponent<WalkForwardCurse>();
            Debug.Log("너 앞으로 되게 잘 걷는다. 계속 앞으로 걸어봐 저주 발동!");
        }
        
        private void Trigger()
        {
            if (FindObjectOfType<BaseManager>().State is not GameState.MainGame) return;
            
            var Player = FindObjectOfType<Player>();
            Player.IsDead = true;
            Debug.Log("저주가 발동되면 AI에게 풀리지 않는 어그로가 발동하여 사망에 이르게 된다");

            GameManager.Instance.CurseGameOver();
        }
    }
}