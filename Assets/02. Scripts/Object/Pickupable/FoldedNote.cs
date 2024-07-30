using UnityEngine;
using UnityEditor;

namespace TT
{
    public class FoldedNote : PickupableObject, IDescribable
    {
        public ClueType ClueType;
        private bool isCurseClue;
        private string description;
        private ICurse appliedCurse;

        private ClueManager clueManager;
        private CurseManager curseManager;

        public bool isTestCurse;
        
        protected override void Awake()
        {
            base.Awake();
            curseManager = FindObjectOfType<CurseManager>();
            clueManager = FindObjectOfType<ClueManager>();

            if (!isTestCurse)
            {
                ClueType clueType = GenerateClueType();
                ClueType = clueType;
            }
            else
            {
                ClueType = ClueType.Curse;
            }

            isCurseClue = ClueType == ClueType.Curse;
            if (isCurseClue)
            {
                appliedCurse = curseManager.GetRandomCurse();
                if (appliedCurse != null)
                {
                    OnPickUpEvent.AddListener(appliedCurse.Activate);
                }
            }
        }

        public string GetDescription()
        {
            return description;
        }

        public void SetDescription(string desc)
        {
            if (desc != null)
            {
                description = desc;
            }
        }

        private ClueType GenerateClueType()
        {
            if (clueManager.CanAddRealClue())
            {
                clueManager.AddRealClue();
                return ClueType.Real;
            }

            int randomValue = Random.Range(0, 10);
            return randomValue < 9 ? ClueType.Fake : ClueType.Curse; // 90% 확률로 가짜 단서, 10% 확률로 저주 단서
        }

        public void ChangeClueType()
        {
            ClueType = ClueType.Fake;
        }

        private void OnDestroy()
        {
            if (isCurseClue && appliedCurse != null)
            {
                OnPickUpEvent.RemoveListener(appliedCurse.Activate);
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = isCurseClue ? Color.red : Color.green;
            Gizmos.DrawWireSphere(transform.position, 0.1f);

            GUIStyle style = new GUIStyle();
            style.normal.textColor = Gizmos.color;
            Handles.Label(transform.position, ClueType == ClueType.Real ? "진짜" : "가짜", style);
        }
#endif
    }
}
