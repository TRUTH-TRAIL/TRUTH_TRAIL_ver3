using System;
using UnityEngine;

namespace TT
{
    public class SpecialPaperCanvas : MonoBehaviour
    {
        public GameObject SpecialPaperCanvasObject;
        
        public GameObject ToChangedPaper;

        private void Awake()
        {
            ClueManager.Instance.OnChangeSpecialPaper += Change;
        }

        [ContextMenu("TestChange")]
        private void Change()
        {
            //SpecialPaperCanvasObject = ToChangedPaper;
        }

        private void OnDestroy()
        {
            ClueManager.Instance.OnChangeSpecialPaper -= Change;
        }
    }
}
