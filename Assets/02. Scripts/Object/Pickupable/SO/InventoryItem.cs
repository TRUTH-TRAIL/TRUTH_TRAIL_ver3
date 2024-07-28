using UnityEngine;

namespace TT
{
    [CreateAssetMenu(fileName = "NewClue", menuName = "TT/InventoryItem")]
    public class InventoryItem : Item
    {
        private void OnEnable()
        {
            ItemType = ItemType.InventoryItem;
        }
    }
}
