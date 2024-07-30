using UnityEngine;

namespace TT
{
    public class OpenBasementDoorCurse : MonoBehaviour, ICurse
    {
        public string Description => "지하실 문을 열어봐";
        
        public void Activate()
        {
            Player.Instance.CurrentCurse = Player.Instance.gameObject.AddComponent<OpenBasementDoorCurse>();
            Debug.Log("지하실 문을 열어봐 저주 발동!");
            // 추가적인 저주 효과 로직
        }
        
        private void Trigger()
        {
            Debug.Log("저주가 발동되면 AI에게 풀리지 않는 어그로가 발동하여 사망에 이르게 된다");
        }
    }
}