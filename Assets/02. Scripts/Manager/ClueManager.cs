using System.Collections.Generic;

namespace TT
{
    public class ClueManager : Singleton<ClueManager>
    {
        public List<string> realClues = new List<string>();
        public List<string> fakeClues = new List<string>();
        public List<string> curseClues = new List<string>();

        private int realClueCount = 0;
        public int MaxRealClues = 10;
        
        public bool CanAddRealClue()
        {
            return realClueCount < MaxRealClues;
        }

        public void AddRealClue()
        {
            if (realClueCount < MaxRealClues)
            {
                realClueCount++;
            }
        }

        public string GetClueDescription(ClueType clueType)
        {
            List<string> clueList = GetClueList(clueType);

            if (clueList.Count > 0)
            {
                string description = clueList[0];
                clueList.RemoveAt(0); // 리스트에서 설명 제거
                return description;
            }

            return "No description available";
        }

        private List<string> GetClueList(ClueType clueType)
        {
            return clueType switch
            {
                ClueType.Real => realClues,
                ClueType.Fake => fakeClues,
                ClueType.Curse => curseClues,
                _ => new List<string>(),
            };
        }
    }
}