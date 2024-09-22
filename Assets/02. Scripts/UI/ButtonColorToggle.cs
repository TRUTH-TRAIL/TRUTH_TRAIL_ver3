using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TT
{
    public class ButtonColorToggle : MonoBehaviour
    {
        public Button targetButton; // Ŭ���� ��ư
        public GameObject OptionsMenu;
        private Color targetColor = new Color(0xDF / 255f, 0xC1 / 255f, 0x8B / 255f); // DFC18B ����
        private Color originalColor = new Color(0xF9 / 255f, 0xE5 / 255f, 0xCF / 255f); // F9E5CF ����
        private bool isTargetColor = false; // ������ ����Ǿ����� ����

        private void Start()
        {
            // ��ư�� �ؽ�Ʈ ������Ʈ ��������
            Text buttonText = targetButton.GetComponentInChildren<Text>();

            // ��ư Ŭ�� �̺�Ʈ ������ �߰�
            targetButton.onClick.AddListener(ToggleButtonTextColor);

            // �ʱ� ���� ���� (optional, �̹� ���� ������ �Ǿ� ���� �� ����)
            if (buttonText != null)
            {
                buttonText.color = originalColor;
            }
        }
        void ToggleButtonTextColor()
        {
            // ��ư�� �ؽ�Ʈ ������Ʈ ��������
            Text buttonText = targetButton.GetComponentInChildren<Text>();

            // ���� ���
            if (buttonText != null)
            {
                if (isTargetColor)
                {
                    buttonText.color = originalColor; // F9E5CF�� �ǵ�����
                    if(OptionsMenu.activeSelf)
                        OptionsMenu.SetActive(false);
                }
                else
                {
                    buttonText.color = targetColor; // DFC18B�� ����
                }

                // ���¸� ����
                isTargetColor = !isTargetColor;
            }
        }
    }
}