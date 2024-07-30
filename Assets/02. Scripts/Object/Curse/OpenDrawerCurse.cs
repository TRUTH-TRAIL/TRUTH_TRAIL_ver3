using UnityEngine;

namespace TT
{
    public class OpenDrawerCurse : MonoBehaviour, ICurse
    {
        public string Description => "서랍을 열어봐";
        
        public void Activate()
        {
            // 저주 발동 로직
            Player.Instance.CurrentCurse = Player.Instance.gameObject.AddComponent<OpenDrawerCurse>();
            Debug.Log("서랍을 열어봐 저주 발동!");
        }
        
        private void Trigger()
        {
            Debug.Log("저주가 발동되면 AI에게 풀리지 않는 어그로가 발동하여 사망에 이르게 된다");
        }
    }
}