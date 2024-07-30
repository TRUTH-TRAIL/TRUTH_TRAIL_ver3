using UnityEngine;

namespace TT
{
    public class BathroomFloorCurse : MonoBehaviour, ICurse
    {
        public string Description => "화장실에 뭔가 있어. 확인해볼래?";

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("RestRoomFloor"))
            {
                Trigger();
            }
        }

        public void Activate()
        {
            Player.Instance.CurrentCurse = Player.Instance.gameObject.AddComponent<WalkForwardCurse>();
            Debug.Log("화장실에 뭔가 있어. 확인해볼래? 저주 발동!");
        }
        
        private void Trigger()
        {
            Debug.Log("저주가 발동되면 AI에게 풀리지 않는 어그로가 발동하여 사망에 이르게 된다");
        }
    }
}