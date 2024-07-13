using UnityEngine;

namespace TT
{
    [CreateAssetMenu(fileName = "NewClue", menuName = "TT/Clue")]
    public class Clue : ScriptableObject
    {
        public string Description;
        public ClueType ClueType;
        public Sprite ClueImage;

        public string GetDescription() => Description;
        public ClueType GetClueType() => ClueType;
        public Sprite GetImage() => ClueImage;
    }
}
