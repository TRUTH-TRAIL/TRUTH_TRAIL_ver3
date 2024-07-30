using UnityEngine;

namespace TT
{
    public class MoveSideCurse : MonoBehaviour, ICurse
    {
        public string Description => "옆으로 움직여 볼래?";

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                Trigger();
            }
        }

        public void Activate()
        {
            // 저주 발동 로직
            Player.Instance.CurrentCurse = Player.Instance.gameObject.AddComponent<MoveSideCurse>();
            Debug.Log("옆으로 움직여 볼래? 저주 발동!");
            // 추가적인 저주 효과 로직
        }
        
        private void Trigger()
        {
            Debug.Log("저주가 발동되면 AI에게 풀리지 않는 어그로가 발동하여 사망에 이르게 된다");
        }
    }
}