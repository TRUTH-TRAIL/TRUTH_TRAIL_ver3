using UnityEngine;

namespace TT
{
    public class PlayerUIHandler : MonoBehaviour
    {
        [Header("UI Elements")]
        public CanvasToggle InventoryUI;
        public CanvasToggle PausePanelUI;
        public GameObject CenterMouseCursor;
        
        [Header("KeyCodes")]
        public KeyCode ToggleInventoryKey = KeyCode.Tab;
        public KeyCode TogglePausePanelKey = KeyCode.Escape;

        private SpecialPaperHandler specialPaperHandler;
        
        private void Awake()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            specialPaperHandler = FindObjectOfType<SpecialPaperHandler>();
        }

        private void Update()
        {
            HandleUI(ToggleInventoryKey, InventoryUI);
            HandleUI(TogglePausePanelKey, PausePanelUI);
        }

        public void HandleUI(KeyCode key, CanvasToggle uiElement, bool IsToggle = false)
        {
            if (Input.GetKeyDown(key) || IsToggle)
            {
                if (uiElement != null)
                {
                    uiElement.Toggle();
                    
                    if (uiElement == InventoryUI)
                    {
                        if (specialPaperHandler.IsSeeState)
                        {
                            specialPaperHandler.IsSeeState = false;
                        }
                        bool isInventoryActive = uiElement.IsActive;
                        Cursor.visible = isInventoryActive;
                        Cursor.lockState = isInventoryActive ? CursorLockMode.None : CursorLockMode.Locked;
                        CenterMouseCursor.SetActive(!isInventoryActive);
                    }
                }
            }
        }
    }
}