using UnityEngine;

namespace TT
{
    public class RemoveBooksCurse : MonoBehaviour, ICurse
    {
        public string Description => "책장의 책을 치워봐";

        public void Activate()
        {
            Player.Instance.CurrentCurse = Player.Instance.gameObject.AddComponent<RemoveBooksCurse>();
            Debug.Log("책장의 책을 치워봐 저주 발동!");
        }
        
        private void Trigger()
        {
            Debug.Log("저주가 발동되면 AI에게 풀리지 않는 어그로가 발동하여 사망에 이르게 된다");
        }
    }
}