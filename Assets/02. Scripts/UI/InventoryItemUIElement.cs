using UnityEngine;
using UnityEngine.UI;

namespace TT
{
    public class InventoryItemUIElement : MonoBehaviour
    {
        public Image ItemImage;
        public Button ItemButton;
        public Button SeeButton;
        public Button EquipButton;

        private bool isActive = false;

        private void Awake()
        {
            ItemButton.onClick.AddListener(() =>
            {
                isActive = !isActive;
            });
            
            ItemButton.onClick.AddListener(SetActiveTrueEquip);
        }

        private void OnDestroy()
        {
            ItemButton.onClick.RemoveAllListeners();
        }

        private void SetActiveTrueEquip()
        {
            EquipButton.gameObject.SetActive(isActive);
        }
        
        public void SetActiveTrueSee()
        {
            ItemButton.onClick.AddListener(() =>
            {
                SeeButton.gameObject.SetActive(isActive);
            });
        }
    }
}