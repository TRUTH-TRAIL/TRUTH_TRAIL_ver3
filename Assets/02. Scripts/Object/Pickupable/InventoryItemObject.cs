using UnityEngine;

namespace TT
{
    public class InventoryItemObject : PickupableObject, IImageable
    {
        public Sprite ItemImage;
        
        public Sprite GetImage()
        {
            return ItemImage;
        }
    }
}
