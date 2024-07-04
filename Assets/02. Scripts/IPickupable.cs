namespace TT
{
    public enum ClueType
    {
        Real,
        Fake,
        Curse
    }
    
    public interface IPickupable
    {
        string GetDescription();
        ClueType GetClueType();
        void OnPickUp();
    }
}
