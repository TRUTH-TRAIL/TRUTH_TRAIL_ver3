using System;
using UnityEditor;
using UnityEngine;

namespace TT
{
    public class PlayerUIHandler : MonoBehaviour
    {
        [Header("UI Elements")]
        public GameObject InventoryUI;
        public GameObject PausePanelUI;
        public GameObject CenterMouseCursor;
        
        [Header("KeyCodes")]
        public KeyCode ToggleInventoryKey = KeyCode.Tab;
        public KeyCode TogglePausePanelKey = KeyCode.Escape;

        private bool isSpecialPaperOn = false;
        private bool isInventoryActive = false;
        private bool isPaused = false;

        private void Awake()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            HandleUI(ToggleInventoryKey, ref isInventoryActive, InventoryUI);
            HandleUI(TogglePausePanelKey, ref isPaused, PausePanelUI);
        }

        private void HandleUI(KeyCode key, ref bool isActive, GameObject uiElement)
        {
            if (Input.GetKeyDown(key))
            {
                isActive = !isActive;
                if (uiElement != null)
                {
                    uiElement.SetActive(isActive);
                    
                    // 인벤토리가 활성화되면 커서를 보이도록 설정
                    if (uiElement == InventoryUI)
                    {
                        Cursor.visible = isActive;
                        Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
                        CenterMouseCursor.SetActive(!isActive);
                    }
                }
            }
        }
    }
}