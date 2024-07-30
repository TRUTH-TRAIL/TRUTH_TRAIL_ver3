using UnityEngine;

namespace TT
{
    public class SpecialPaperHandler : MonoBehaviour
    {
        [Header("Clue Count")] 
        public int MaxClues = 10;

        [Header("습득물보관(특수용지)")] 
        public CanvasToggle SpecialPaper;
        public GameObject SpecialPaperImage;
        public KeyCode OpenSpecialPaperKeyCode = KeyCode.R;

        [Header("장착 시")] 
        public bool IsEquipped;
        public Vector2 EquippedPosition = new Vector3(491.600006f,-508.589996f,0);
        public Quaternion EquippedRotation = Quaternion.Euler(0,0,27.3699989f);
        private Vector2 originalPosition = new Vector2(0,0);
        private Quaternion originalRotation = Quaternion.Euler(0,0,0);
        
        private bool isActiveState = false;
        public bool IsSeeState;

        public bool TryAddClue(IPickupable item)
        {
            if (item == null) return false;

            FoldedNote note = item as FoldedNote;

            if (note.ClueType == ClueType.Curse && !Player.Instance.IsCursed) //저주에 걸린 상태가 아니라면
            {
                ClueManager.Instance.CurrentCurse = note;
                ICurse curse = CurseManager.Instance.GetRandomCurse();
                if (curse != null)
                {
                    note.SetDescription(curse.Description);
                    ClueManager.Instance.CurrentCurse = note;
                    curse.Activate();
                }
            }
            else if (note.ClueType == ClueType.Curse && Player.Instance.IsCursed) //이미 저주에 걸린 상태라면
            {
                note.ChangeClueType();
            }
            
            if (ClueManager.Instance.Clues.Count >= MaxClues)
            {
                StartCoroutine(ClueManager.Instance.DisplayLog("인벤토리 꽉참."));
                return false;
            }

            if (note.ClueType == ClueType.Real)
            {
                ClueManager.Instance.GetRealClue();
            }

            // 새로운 단서에만 설명 설정
            if (string.IsNullOrEmpty(note.GetDescription()))
            {
                note.SetDescription(ClueManager.Instance.GetClueDescription(note.ClueType));
            }

            ClueManager.Instance.AddClue(note);
            
            return true;
        }

        private bool isJustOnce = false;
        
        private void Update()
        {
            if (!Player.Instance.isAcquiredSpecialPaper) return;

            if (Input.GetKey(OpenSpecialPaperKeyCode) || IsSeeState)
            {
                if (!isJustOnce)
                {
                    SpecialPaper.Toggle();
                    isJustOnce = true;
                }
            }
            else if (Input.GetKeyUp(OpenSpecialPaperKeyCode))
            {
                SpecialPaper.Toggle();
                isJustOnce = false;
            }

            Player.Instance.isEqiupSpecialPaper = IsEquipped;
            SpecialPaperImage.transform.localPosition = IsEquipped ? EquippedPosition : originalPosition;
            SpecialPaperImage.transform.localRotation = IsEquipped ? EquippedRotation : originalRotation;
        }
    }
}
