using UnityEngine;

namespace TT
{
    public class WalkForwardCurse : MonoBehaviour, ICurse
    {
        public string Description => "너 앞으로 되게 잘 걷는다. 계속 앞으로 걸어봐";

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Trigger();
            }
        }

        public void Activate()
        {
            Player.Instance.CurrentCurse = Player.Instance.gameObject.AddComponent<WalkForwardCurse>();
            Debug.Log("너 앞으로 되게 잘 걷는다. 계속 앞으로 걸어봐 저주 발동!");
        }
        
        private void Trigger()
        {
            Debug.Log("저주가 발동되면 AI에게 풀리지 않는 어그로가 발동하여 사망에 이르게 된다");
        }
    }
}