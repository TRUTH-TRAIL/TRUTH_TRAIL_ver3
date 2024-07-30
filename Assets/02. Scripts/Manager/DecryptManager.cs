using System;
using System.Collections.Generic;
using UnityEngine;

namespace TT
{
    public class DecryptManager : MonoBehaviour
    {
        [SerializeField] private ClueManager clueManager;

        private void Awake()
        {
            clueManager = FindObjectOfType<ClueManager>();
        }

        public void RemoveFalseAndCurseClues()
        {
            List<FoldedNote> trueClues = new List<FoldedNote>();

            foreach (var clue in clueManager.Clues)
            {
                FoldedNote note = clue;
                if (note != null && note.ClueType == ClueType.Real)
                {
                    trueClues.Add(clue);
                }
            }

            Player.Instance.RemoveComponent();
            clueManager.Decrypt(trueClues);
        }
    }
}
