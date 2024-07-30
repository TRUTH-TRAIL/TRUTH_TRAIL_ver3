using UnityEngine;

namespace TT
{
    public class LookBackCurse : ICurse
    {
        public string Description => "뒤돌아봐";
        private GameObject player;

        public LookBackCurse(GameObject player)
        {
            this.player = player;
        }

        public bool CanActivate()
        {
            // 1초 안에 카메라 180도 회전하는 조건
            return true; // 임시로 항상 true로 설정
        }

        public void Activate()
        {
            if (CanActivate())
            {
                Debug.Log("뒤돌아봐 저주 발동!");
                // 저주 발동 로직 추가
            }
        }
    }
}
