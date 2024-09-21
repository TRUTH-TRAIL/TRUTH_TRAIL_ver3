using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TT
{
    public class InventoryItemHandler : MonoBehaviour
    {
        public InventoryItemObject CurrentHeldInventoryItem;
        public List<InventoryItemUIElement> InventoryItemUIElements;

        [Header("장착 시")] 
        public bool IsEquipped;
        public Vector2 EquippedPosition = new Vector3(491.6f, -508.59f, 0);
        public Quaternion EquippedRotation = Quaternion.Euler(0, 0, 27.37f);

        private readonly Vector2 originalPosition = new Vector2(0, 0);
        private readonly Quaternion originalRotation = Quaternion.Euler(0, 0, 0);

        public CanvasToggle SpecialPaper;
        public GameObject SpecialPaperImage;

        public CanvasToggle InventoryItem;
        public GameObject InventoryItemImage;

        private CanvasToggle previousCanvas;
        
        public void ChangeEquippedItem(InventoryItemObject item, InventoryItemUIElement itemUIElement, bool isEquipped)
        {
            if (previousCanvas is not null)
            {
                previousCanvas.Off();
            }
            
            //Init
            foreach (var element in InventoryItemUIElements)
            {
                if(element == itemUIElement) continue;
                element.IsEquipped = false;
            }
     
            if (item != null)
            {
                CurrentHeldInventoryItem = item;
            }

            IsEquipped = !isEquipped;

            if (IsEquipped)
            {
                HandleEquipped();
            }
            else
            {
                HandleUnequipped();
            }
            
            foreach (var element in InventoryItemUIElements)
            {
                element.UpdatePlayerEquipmentStatus();
            }
        }

        private void HandleEquipped()
        {
            if (CurrentHeldInventoryItem.name == "SpecialPaper")
            {
                Debug.Log($"Toggle 1");
                SpecialPaper.Toggle();  
                SetItemTransform(SpecialPaperImage, EquippedPosition, EquippedRotation);
                previousCanvas = SpecialPaper;
            }
            else
            {
                InventoryItemImage.GetComponentInChildren<Image>().sprite = CurrentHeldInventoryItem.GetImage();
                Handle3DEquipped();
                previousCanvas = InventoryItem;
            }
        }

        private void HandleUnequipped()
        {
            if (CurrentHeldInventoryItem.name == "SpecialPaper")
            {
                Debug.Log($"Toggle 2");
                SpecialPaper.Off(); 
                SetItemTransform(SpecialPaperImage, originalPosition, originalRotation);
            }
            else
            {
                Handle3DUnequipped();
            }
        }

        private void SetItemTransform(GameObject item, Vector2 position, Quaternion rotation)
        {
            item.transform.localPosition = position;
            item.transform.localRotation = rotation;
        }

        private void Handle3DEquipped()
        {
            InventoryItem.Toggle();
            SetItemTransform(InventoryItemImage, EquippedPosition, EquippedRotation);
        }

        private void Handle3DUnequipped()
        {
            InventoryItem.Off();
            SetItemTransform(InventoryItemImage, originalPosition, originalRotation);
        }
    }
}
