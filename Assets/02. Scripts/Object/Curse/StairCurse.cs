using UnityEngine;

namespace TT
{
    public class StairCurse : MonoBehaviour, ICurse
    {
        public string Description => "계단으로 와볼래?";
        private GameObject player;

        public StairCurse(GameObject player)
        {
            this.player = player;
        }
        
        public void Activate()
        {
            // 저주 발동 로직
            var Player = FindObjectOfType<Player>();
            Player.CurrentCurse = player.AddComponent<StairCurse>();
            Debug.Log("계단 저주 발동!");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Stair"))
            {
                Trigger();
            }
        }

        private void Trigger()
        {
            if (FindObjectOfType<BaseManager>().State is not GameState.MainGame) return;
            
            var Player = FindObjectOfType<Player>();
            Player.IsDeadCurseState = true;
            Debug.Log("저주가 발동되면 AI에게 풀리지 않는 어그로가 발동하여 사망에 이르게 된다");

            GameManager.Instance.CurseGameOver();
        }
    }

}
