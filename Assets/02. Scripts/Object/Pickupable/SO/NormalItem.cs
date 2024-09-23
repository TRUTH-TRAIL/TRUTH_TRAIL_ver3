using UnityEngine;

namespace TT
{
    [CreateAssetMenu(fileName = "NewClue", menuName = "TT/NormalItem")]
    public class NormalItem : Item
    {
        private void OnEnable()
        {
            ItemType = ItemType.Normal;
        }
    }
}