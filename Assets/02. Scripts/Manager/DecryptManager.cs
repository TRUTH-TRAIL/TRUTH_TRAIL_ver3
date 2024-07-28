using System.Collections.Generic;
using UnityEngine;

namespace TT
{
    public class DecryptManager : Singleton<DecryptManager>
    {
        public void RemoveFalseAndCurseClues(SpecialPaperController specialPaper)
        {
            List<IPickupable> trueClues = new List<IPickupable>();

            foreach (var clue in specialPaper.clues)
            {
                FoldedNote note = clue as FoldedNote;
                if (note != null && note.ClueType == ClueType.Real)
                {
                    trueClues.Add(clue);
                }
            }

            specialPaper.clues = trueClues;
            Debug.Log("거짓과 저주 단서가 제거되었습니다.");

            specialPaper.UpdateClueUI();
        }
    }
}
