using System;
using UnityEngine;
using System.Collections;

namespace TT
{
    public class LookBackCurse : MonoBehaviour, ICurse
    {
        public string Description => "뒤돌아봐";
        private GameObject player;
        private Quaternion initialRotation;
        private bool isTriggered;

        private void Awake()
        {
            StartCoroutine(CheckRotation());
            isTriggered = false;
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        public LookBackCurse(GameObject player)
        {
            this.player = player;
        }

        public void Activate()
        {
            FindObjectOfType<Player>().CurrentCurse = player.AddComponent<LookBackCurse>();
            Debug.Log("뒤돌아봐 저주 발동!");
        }

        private IEnumerator CheckRotation()
        {
            while (!isTriggered)
            {
                initialRotation = transform.rotation;
                float elapsedTime = 0f;

                while (elapsedTime < 1f)
                {
                    elapsedTime += Time.deltaTime;
                    float angle = Quaternion.Angle(initialRotation, transform.rotation);
                    if (angle >= 120f) //180f 절대 안됨
                    {
                        Trigger();
                        isTriggered = true;
                        break;
                    }
                    yield return null;
                }

                if (!isTriggered)
                {
                    Debug.Log("카메라가 1초 안에 180도 회전하지 않았습니다. 초기화 중...");
                }
            }
        }

        private void Trigger()
        {
            
            FindObjectOfType<Player>().IsDeadCurseState = true;
            Debug.Log("저주가 발동되면 AI에게 풀리지 않는 어그로가 발동하여 사망에 이르게 된다");
        }
    }
}