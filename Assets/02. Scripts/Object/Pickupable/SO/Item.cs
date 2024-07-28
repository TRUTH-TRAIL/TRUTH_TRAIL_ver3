using UnityEngine;
using UnityEngine.UI;

namespace TT
{
    public class Item : ScriptableObject
    {
        public string Description;

        public ItemType ItemType;
        public Image ClueImage;

        public string GetDescription() => Description;
        public ItemType GetItemType() => ItemType;
        public Image GetImage() => ClueImage;
    }
}
