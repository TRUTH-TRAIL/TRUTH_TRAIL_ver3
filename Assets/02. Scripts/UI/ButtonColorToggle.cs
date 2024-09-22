using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TT
{
    public class ButtonColorToggle : MonoBehaviour
    {
        public Button targetButton; // 클릭할 버튼
        public GameObject OptionsMenu;
        private Color targetColor = new Color(0xDF / 255f, 0xC1 / 255f, 0x8B / 255f); // DFC18B 색상
        private Color originalColor = new Color(0xF9 / 255f, 0xE5 / 255f, 0xCF / 255f); // F9E5CF 색상
        private bool isTargetColor = false; // 색상이 변경되었는지 여부

        private void Start()
        {
            // 버튼의 텍스트 컴포넌트 가져오기
            Text buttonText = targetButton.GetComponentInChildren<Text>();

            // 버튼 클릭 이벤트 리스너 추가
            targetButton.onClick.AddListener(ToggleButtonTextColor);

            // 초기 색상 설정 (optional, 이미 원래 색으로 되어 있을 수 있음)
            if (buttonText != null)
            {
                buttonText.color = originalColor;
            }
        }
        void ToggleButtonTextColor()
        {
            // 버튼의 텍스트 컴포넌트 가져오기
            Text buttonText = targetButton.GetComponentInChildren<Text>();

            // 색상 토글
            if (buttonText != null)
            {
                if (isTargetColor)
                {
                    buttonText.color = originalColor; // F9E5CF로 되돌리기
                    if(OptionsMenu.activeSelf)
                        OptionsMenu.SetActive(false);
                }
                else
                {
                    buttonText.color = targetColor; // DFC18B로 변경
                }

                // 상태를 반전
                isTargetColor = !isTargetColor;
            }
        }
    }
}