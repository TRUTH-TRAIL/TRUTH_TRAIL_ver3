using System.Collections.Generic;

namespace TT
{
    public class DecryptManager : Singleton<DecryptManager>
    {
        public void RemoveFalseAndCurseClues()
        {
            List<FoldedNote> trueClues = new List<FoldedNote>();

            foreach (var clue in ClueManager.Instance.Clues)
            {
                FoldedNote note = clue;
                if (note != null && note.ClueType == ClueType.Real)
                {
                    trueClues.Add(clue);
                }
            }

            Player.Instance.IsCursed = false;
            ClueManager.Instance.Decrypt(trueClues);
        }
    }
}
