using UnityEngine;

namespace TT
{
    public class Item : ScriptableObject
    {
        public string Description;

        public ItemType ItemType;
        public Sprite ClueImage;

        public string GetDescription() => Description;
        public ItemType GetItemType() => ItemType;
        public Sprite GetImage() => ClueImage;
    }
}
