using UnityEngine;

namespace TT
{
    public class InventoryUIHandler : MonoBehaviour
    {
        public Transform Root;
        
        [Header("생성되는 UI Prefab")]
        public GameObject InventoryItemUIPrefab;

        public GameObject InitInventoryItem;
        
        private void Start()
        {
            if (Player.Instance.isAcquiredSpecialPaper)
            {
                IPickupable pickupable = InitInventoryItem.GetComponentInChildren<IPickupable>();
                TryAddInventoryItem(pickupable);
            }
        }

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
            inventoryItemUIElement.ItemImage.sprite = inventoryItemObject?.GetImage();
            inventoryItemUIElement.Inject(inventoryItemObject);
            
            //오로지 특수용지만을 위한
            if (item is SpecialPaper)
            {
                inventoryItemUIElement.SetActiveSeeButton();
            }
        }
    }
}
