using UnityEngine;

namespace TT
{
    [CreateAssetMenu(fileName = "NewClue", menuName = "TT/Item")]
    public class Item : ScriptableObject
    {
        public string Description;
        public ClueType ClueType;
        public ItemType ItemType;
        public Sprite ClueImage;

        public string GetDescription() => Description;
        public ClueType GetClueType() => ClueType;
        public ItemType GetItemType() => ItemType;
        public Sprite GetImage() => ClueImage;
    }
}
