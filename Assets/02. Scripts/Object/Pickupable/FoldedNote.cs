using UnityEditor;
using UnityEngine;

namespace TT
{
    public class FoldedNote : PickupableObject, IDescribable
    {
        public ClueType ClueType;
        
        private bool isCurseClue;
        
        protected override void Awake()
        {
            base.Awake();
            ClueType clueType = GenerateRandomClueType();
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
            return ClueManager.Instance.GetClueDescription(ClueType);
        }
        
        private ClueType GenerateRandomClueType()
        {
            int randomValue = Random.Range(0, 10);

            return randomValue switch
            {
                < 2 => ClueType.Real,
                < 8 => ClueType.Fake,
                _ => ClueType.Curse,
            };
        }
        private void ApplyCurse()
        {
            if (!Player.Instance.IsCursed)
            {
                //저주 입히기 로직 추가
                Player.Instance.IsCursed = true;
                Debug.Log("ㅋㅋㅋ너 저주걸림");
            }
        }

        public void ChangeClueType()
        {
            //item도 바꿔줘야지 아니면 문구 안달라짐 ㅅㅂ
            ClueType clueType = ClueType.Fake;
            ClueType = clueType;
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
            Gizmos.DrawWireSphere(transform.position, 0.5f);

#if UNITY_EDITOR
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Gizmos.color;
            Handles.Label(transform.position + Vector3.up * 0.5f, isCurseClue ? "저주" : "저주아님", style);
#endif
        }

        
    }
}
