using UnityEngine.UI;

namespace TT
{
    public enum ClueType
    {
        Real,
        Fake,
        Curse
    }

    public enum ItemType
    {
        Clue,
        InventoryItem,
    }
    
    public interface IDescribable
    {
        string GetDescription();
    }

    public interface IImageable
    {
        Image GetImage();
    }
    
    public interface IPickupable
    {
        ItemType GetItemType();
        void OnPickUp();
    }
}
