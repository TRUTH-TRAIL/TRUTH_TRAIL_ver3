using UnityEngine;

namespace TT
{
    public class SpecialPaperChangeController : MonoBehaviour
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
