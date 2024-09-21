using UnityEngine;

namespace TT
{
    public enum InventoryItemType
    {
        SpecialPaper = 0,
        Battery = 1,
        Key = 2,
        Lighter = 3,
        Cross = 4,
        SpecialCandle = 5,
    }
    
    public class InventoryItemObject : PickupableObject, IImageable
    {
        public InventoryItemType Type;
        
        public Sprite GetImage()
        {
            return item.GetImage();;
        }
    }
}
