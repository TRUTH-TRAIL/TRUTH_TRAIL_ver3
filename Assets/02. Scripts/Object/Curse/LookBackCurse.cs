using UnityEngine;

namespace TT
{
    public class LookBackCurse : MonoBehaviour, ICurse
    {
        public string Description => "뒤돌아봐";
        private GameObject player;

        public LookBackCurse(GameObject player)
        {
            this.player = player;
        }

        public void Activate()
        {
            Player.Instance.CurrentCurse = player.AddComponent<LookBackCurse>();
            // 1초 안에 카메라 180도 회전하는 조건
            Debug.Log("뒤돌아봐 저주 발동!");
        }
        
        private void Trigger()
        {
            Debug.Log("저주가 발동되면 AI에게 풀리지 않는 어그로가 발동하여 사망에 이르게 된다");
        }
    }
}
