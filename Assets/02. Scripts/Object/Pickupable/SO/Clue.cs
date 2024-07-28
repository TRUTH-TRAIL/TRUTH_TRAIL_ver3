using UnityEngine;

namespace TT
{
    [CreateAssetMenu(fileName = "NewClue", menuName = "TT/Clue")]
    public class Clue : Item
    {
        public ClueType ClueType;
        public ClueType GetClueType() => ClueType;
        
        private void OnEnable()
        {
            ItemType = ItemType.Clue;
        }
    }
}
