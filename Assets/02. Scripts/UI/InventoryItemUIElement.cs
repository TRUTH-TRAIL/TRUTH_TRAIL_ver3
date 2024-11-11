using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TT
{
    public class InventoryItemUIElement : MonoBehaviour
    {
        public string ItemName;
        public Image ItemImage;
        public Button ItemButton;
        public Button SeeButton;
        public Button EquipButton;

        [SerializeField] private TextMeshProUGUI equipButtonText;
        
        private SpecialPaperHandler specialPaperHandler;
        private PlayerUIHandler playerUIHandler;
        private InventoryItemHandler inventoryItemHandler;
        private InventoryItemObject inventoryItemObject;
        
        private string itemDescription;
        private bool isActive = false;
        public bool IsEquipped { get; set; }
        
        private void AfterInject()
        {
            if(inventoryItemObject.item.name == "Battery")
            {
                Player.Instance.isAcquiredBattery = true;
                return;
            }
            ItemButton.onClick.AddListener(ToggleIsActive);
            ItemButton.onClick.AddListener(SetActiveEquipButton);
            
            InitializeSpecialPaperHandler();
            InitializePlayerUIHandler();
            InitializeInventoryItemHandler();

            playerUIHandler.OnToggleUI += Off;
            
            EquipButton.onClick.AddListener(ToggleEquipped);
            
            inventoryItemHandler.InventoryItemUIElements.Add(this);
        }

        private void OnDestroy()
        {
            ItemButton.onClick.RemoveAllListeners();
            if (playerUIHandler is not null)
            {
                playerUIHandler.OnToggleUI -= Off;    
            }
        }

        private void ToggleIsActive()
        {
            isActive = !isActive;
        }
        
        public void Inject(InventoryItemObject item)
        {
            ItemName = item.item.name;
            itemDescription = item.item.name;
            inventoryItemObject = item;
            
            AfterInject();
        }
        
        private void SetActiveEquipButton()
        {
            EquipButton.gameObject.SetActive(isActive);
            equipButtonText.text = IsEquipped ? "장착해제" : "장착";
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
            if (itemDescription == "SpecialPaper")
            {
                ToggleInventoryState();
                ToggleSpecialPaperSeeState();
            }
        }

        private void ToggleEquipped()
        {
            ChangeEquippedItem();
            ToggleInventoryState();
        }
        
        private void InitializeInventoryItemHandler()
        {
            if (inventoryItemHandler is null)
            {
                inventoryItemHandler = FindObjectOfType<InventoryItemHandler>();
            }
        }
        
        private void InitializePlayerUIHandler()
        {
            if (playerUIHandler is null)
            {
                playerUIHandler = FindObjectOfType<PlayerUIHandler>();
            }
        }

        private void InitializeSpecialPaperHandler()
        {
            if (specialPaperHandler is null)
            {
                specialPaperHandler = FindObjectOfType<SpecialPaperHandler>();
            }
        }

        private void ToggleInventoryState()
        {
            Off();
            playerUIHandler?.HandleUI(playerUIHandler.ToggleInventoryKey, playerUIHandler.InventoryUI, true);
        }
        
        private void ToggleSpecialPaperSeeState()
        {
            if (specialPaperHandler is not null)
            {
                inventoryItemHandler.IsEquipped = false;
                specialPaperHandler.IsSeeState = !specialPaperHandler.IsSeeState;
                Player.Instance.isEqiupSpecialPaper = false;
            }
        }

        private void ChangeEquippedItem()
        {
            inventoryItemHandler.ChangeEquippedItem(inventoryItemObject, this, IsEquipped);
            IsEquipped = !IsEquipped;
            equipButtonText.text = IsEquipped ? "장착해제" : "장착"; // Todo :  장착해제 / 장착
            UpdatePlayerEquipmentStatus();
        }

        public void UpdatePlayerEquipmentStatus()
        {
            switch (inventoryItemObject.item.name)
            {
                case "SpecialPaper":
                    Player.Instance.isEqiupSpecialPaper = IsEquipped;
                    break;
                case "Cross":
                    Player.Instance.isEqiupCross = IsEquipped;
                    break;
                case "Lighter":
                    Player.Instance.isEqiupLighter = IsEquipped;
                    break;
                case "Key":
                    Player.Instance.isEqiupKey = IsEquipped;
                    break;
                case "SpecialCandle1":
                    Player.Instance.isEqiupSpecialCandle1 = IsEquipped;
                    break;
                case "SpecialCandle2":
                    Player.Instance.isEqiupSpecialCandle2 = IsEquipped;
                    break;
                case "SpecialCandle3":
                    Player.Instance.isEqiupSpecialCandle3 = IsEquipped;
                    break;
            }
        }
        
        private void Off()
        {
            EquipButton.gameObject.SetActive(false);
            SeeButton.gameObject.SetActive(false);    
        }
    }
}
