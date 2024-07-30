using UnityEngine;

namespace TT
{
    public class StairCurse : ICurse
    {
        public string Description => "계단으로 와볼래?";
        private GameObject player;

        public StairCurse(GameObject player)
        {
            this.player = player;
        }

        public bool CanActivate()
        {
            // 플레이어가 계단에 닿았는지 확인하는 로직
            return true; //player.GetComponent<Player>().IsOnStairs;
        }

        public void Activate()
        {
            // 저주 발동 로직
            Debug.Log("계단 저주 발동!");
        }
    }

}
