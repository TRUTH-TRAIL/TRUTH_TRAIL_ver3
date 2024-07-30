using UnityEngine;

namespace TT
{
    public class InventoryHandler : MonoBehaviour
    {
        public Transform Root;
        
        [Header("생성되는 UI Prefab")]
        public GameObject InventoryItemUIPrefab;
        
        public bool TryAddInventoryItem(IPickupable item)
        {
            if (item == null) return false;
            
            UpdateInventoryUI(item);
            return true;
        }

        private void UpdateInventoryUI(IPickupable item)
        {
            GameObject inventoryItemUI = Instantiate(InventoryItemUIPrefab, Root);
            inventoryItemUI.transform.localScale = Vector3.one;
            
            var inventoryItemObject = item as InventoryItemObject;
            var inventoryItemUIElement = inventoryItemUI.GetComponent<InventoryItemUIElement>();
            inventoryItemUIElement.ItemImage.sprite = inventoryItemObject.GetImage();
            
            if (item is SpecialPaper)
            {
                inventoryItemUIElement.SetActiveTrueSee();
            }
        }
    }
}
