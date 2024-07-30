using UnityEngine;
using UnityEngine.EventSystems;

namespace TT
{
    public class DoubleClickDoorCurse : MonoBehaviour, ICurse
    {
        public string Description => "방 문을 두번 열어봐";
        private int clickCount = 0;
        private float clickTimer = 0f;
        private const float doubleClickThreshold = 0.5f;

        private void OnClick()
        {
            clickCount++;
            if (clickCount == 1)
            {
                clickTimer = Time.time;
            }
            else if (clickCount == 2 && Time.time - clickTimer < doubleClickThreshold)
            {
                Trigger();
                clickCount = 0;
            }
        }

        private void Update()
        {
            if (clickCount > 0 && Time.time - clickTimer > doubleClickThreshold)
            {
                clickCount = 0;
            }

            if (Input.GetMouseButtonDown(0))
            {
                OnClick();
            }
        }

        public void Activate()
        {
            Player.Instance.CurrentCurse = Player.Instance.gameObject.AddComponent<DoubleClickDoorCurse>();
            Debug.Log("방 문을 두번 열어봐 저주 발동!");
        }
        
        private void Trigger()
        {
            Debug.Log("저주가 발동되면 AI에게 풀리지 않는 어그로가 발동하여 사망에 이르게 된다");
        }
    }
}