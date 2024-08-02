using UnityEngine;
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
        Normal,
    }
    
    public interface IDescribable
    {
        string GetDescription();
    }

    public interface IImageable
    {
        Sprite GetImage();
    }
    
    public interface IPickupable
    {
        ItemType GetItemType();
        void OnPickUp();
    }
}
