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
        string GetDescription();
        ClueType GetClueType();
        ItemType GetItemType();
        void OnPickUp();
    }
}
