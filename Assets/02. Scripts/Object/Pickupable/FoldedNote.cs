using UnityEngine;
using UnityEditor;

namespace TT
{
    public class FoldedNote : PickupableObject, IDescribable
    {
        public ClueType ClueType;
        private bool isCurseClue;
        private string description;

        protected override void Awake()
        {
            base.Awake();
            ClueType clueType = GenerateClueType();
            ClueType = clueType;
            Debug.Log($"나는 어떤 단서일까요~? : {clueType}");

            isCurseClue = ClueType == ClueType.Curse;
            if (isCurseClue)
            {
                OnPickUpEvent.AddListener(ApplyCurse);
            }
        }

        public string GetDescription()
        {
            return description;
        }

        public void SetDescription(string desc)
        {
            description = desc;
        }

        private ClueType GenerateClueType()
        {
            if (ClueManager.Instance.CanAddRealClue())
            {
                ClueManager.Instance.AddRealClue();
                return ClueType.Real;
            }

            int randomValue = Random.Range(0, 10);
            return randomValue < 9 ? ClueType.Fake : ClueType.Curse; // 90% 확률로 가짜 단서, 10% 확률로 저주 단서
        }

        private void ApplyCurse()
        {
            if (!Player.Instance.IsCursed)
            {
                Player.Instance.IsCursed = true;
                Debug.Log("ㅋㅋㅋ너 저주걸림");
            }
        }

        public void ChangeClueType()
        {
            ClueType = ClueType.Fake;
        }

        private void OnDestroy()
        {
            if (isCurseClue)
            {
                OnPickUpEvent.RemoveListener(ApplyCurse);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = isCurseClue ? Color.red : Color.green;
            Gizmos.DrawWireSphere(transform.position, 0.1f);

#if UNITY_EDITOR
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Gizmos.color;
            Handles.Label(transform.position, "단서", style);
#endif
        }
    }
}
