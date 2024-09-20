using TMPro;
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

        [SerializeField] private TextMeshProUGUI equipButtonText;
        
        private bool isActive = false;
        private SpecialPaperHandler specialPaperHandler;
        private PlayerUIHandler playerUIHandler;
        
        private void Awake()
        {
            ItemButton.onClick.AddListener(() =>
            {
                isActive = !isActive;
            });
        
            ItemButton.onClick.AddListener(SetActiveEquipButton);
        }

        private void OnDestroy()
        {
            ItemButton.onClick.RemoveAllListeners();
        }

        private void SetActiveEquipButton()
        {
            EquipButton.gameObject.SetActive(isActive);
            EquipButton.onClick.AddListener(ToggleEquipped);
        }
        
        public void SetActiveSeeButton()
        {
            ItemButton.onClick.AddListener(() =>
            {
                SeeButton.gameObject.SetActive(isActive);    
            });
            SeeButton.onClick.AddListener(ToggleSee);
        }

       
        private void ToggleSee()
        {
            if (specialPaperHandler is null)
            {
                specialPaperHandler = FindObjectOfType<SpecialPaperHandler>();
            }
            ToggleInventory();
            
            if (specialPaperHandler is not null)
            {
                specialPaperHandler.IsSeeState = !specialPaperHandler.IsSeeState;
            }
        }
        
        private void ToggleEquipped()
        {
            //장착 아이템이 여러개므로 아래 리팩토링 수정 필요
            if (specialPaperHandler is null)
            {
                specialPaperHandler = FindObjectOfType<SpecialPaperHandler>();
            }
            
            if (specialPaperHandler is not null)
            {
                specialPaperHandler.IsEquipped = !specialPaperHandler.IsEquipped;
                equipButtonText.text = specialPaperHandler.IsEquipped ? "장착 해제하기" : "장착하기";
            }

            ToggleInventory();
        }

        private void ToggleInventory()
        {
            if (playerUIHandler is null)
            {
                playerUIHandler = FindObjectOfType<PlayerUIHandler>();
            }
            playerUIHandler?.HandleUI(playerUIHandler.ToggleInventoryKey, playerUIHandler.InventoryUI, true);
        }
    }
}
