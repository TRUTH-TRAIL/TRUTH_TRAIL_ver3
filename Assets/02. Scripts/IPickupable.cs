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
    
    public interface IPickupable
    {
        ItemType GetItemType();
        void OnPickUp();
    }
}
