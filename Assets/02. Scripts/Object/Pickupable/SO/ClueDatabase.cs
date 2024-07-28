using UnityEngine;
using System.Collections.Generic;

namespace TT
{
    [CreateAssetMenu(fileName = "ClueDatabase", menuName = "TT/ClueDatabase")]
    public class ClueDatabase : ScriptableObject
    {
        public List<Item> Clues;
    }
}