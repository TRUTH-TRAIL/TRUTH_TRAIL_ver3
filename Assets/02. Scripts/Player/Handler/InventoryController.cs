using UnityEngine;
using UnityEngine.UI;

namespace TT
{
    public class InventoryController : MonoBehaviour
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

            var _ = item as InventoryItemObject;
            //inventoryItemUI.GetComponentInChildren<TextMeshProUGUI>().text = item.GetDescription();
            inventoryItemUI.GetComponentInChildren<Image>().sprite = _.GetImage().sprite;

            inventoryItemUI.gameObject.SetActive(true);
        }
    }
}
