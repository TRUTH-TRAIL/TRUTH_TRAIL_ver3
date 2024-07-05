using UnityEngine;

namespace TT
{
    public class PlayerUIHandler : MonoBehaviour
    {
        [Header("UI Elements")]
        public GameObject SpecialPaper;
        public GameObject InventoryUI;
        public GameObject PausePanelUI;

        [Header("KeyCodes")]
        public KeyCode OpenSpecialPaperKey = KeyCode.R;
        public KeyCode ToggleInventoryKey = KeyCode.Tab;
        public KeyCode TogglePausePanelKey = KeyCode.Escape;

        private bool isSpecialPaperOn = false;
        private bool isInventoryActive = false;
        private bool isPaused = false;

        private void Update()
        {
            HandleUI(OpenSpecialPaperKey, ref isSpecialPaperOn, SpecialPaper);
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
                }
            }
        }
    }
}