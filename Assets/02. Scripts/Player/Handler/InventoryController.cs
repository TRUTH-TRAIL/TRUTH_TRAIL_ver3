using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace TT
{
    public class InventoryController : MonoBehaviour
    {
        public Transform Root;
        
        [Header("생성되는 UI Prefab")]
        public GameObject InventoryItemUIPrefab;
        
        public bool TryAddInventoryItem(Item item)
        {
            if (item == null) return false;
            
            UpdateInventoryUI(item);
            return true;
        }

        private void UpdateInventoryUI(Item item)
        {
            GameObject inventoryItemUI = Instantiate(InventoryItemUIPrefab, Root);

            //inventoryItemUI.GetComponentInChildren<TextMeshProUGUI>().text = item.GetDescription();
            inventoryItemUI.GetComponentInChildren<Image>().sprite = item.GetImage();

            inventoryItemUI.gameObject.SetActive(true);
        }
    }
}
