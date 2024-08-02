using System;
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

        private ClueManager clueManager;
        private CurseManager curseManager;
        private Player Player;
        
        private void Awake()
        {
            Player = FindObjectOfType<Player>();
            clueManager = FindObjectOfType<ClueManager>();
            curseManager = FindObjectOfType<CurseManager>();
        }

        public bool TryAddClue(IPickupable item)
        {
            if (item == null) return false;

            FoldedNote note = item as FoldedNote;
            
            if (note.ClueType == ClueType.Curse && !Player.IsCursed) //저주에 걸린 상태가 아니라면
            {
                clueManager.CurrentCurse = note;
                ICurse curse = curseManager.GetRandomCurse();
                if (curse != null)
                {
                    note.SetDescription(curse.Description);
                    clueManager.CurrentCurse = note;
                    curse.Activate();
                }
            }
            else if (note.ClueType == ClueType.Curse && Player.IsCursed) //이미 저주에 걸린 상태라면
            {
                note.ChangeClueType();
            }
            
            if (clueManager.Clues.Count >= MaxClues)
            {
                StartCoroutine(clueManager.DisplayLog("인벤토리 꽉참."));
                return false;
            }

            if (note.ClueType == ClueType.Real)
            {
                clueManager.GetRealClue();
            }

            // 새로운 단서에만 설명 설정
            if (string.IsNullOrEmpty(note.GetDescription()))
            {
                note.SetDescription(clueManager.GetClueDescription(note.ClueType));
            }

            clueManager.AddClue(note);
            
            return true;
        }

        private bool isJustOnce = false;
        
        private void Update()
        {
            if (!Player.isAcquiredSpecialPaper) return;

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

            Player.isEqiupSpecialPaper = IsEquipped;
            SpecialPaperImage.transform.localPosition = IsEquipped ? EquippedPosition : originalPosition;
            SpecialPaperImage.transform.localRotation = IsEquipped ? EquippedRotation : originalRotation;
        }
    }
}