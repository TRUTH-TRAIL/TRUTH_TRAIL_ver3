using UnityEngine;

namespace TT
{
    public class SpecialPaperChangeController : MonoBehaviour
    {
        public GameObject SpecialPaperCanvasObject;
        
        public GameObject ToChangedPaper;

        private ClueManager clueManager;
        
        private void Awake()
        {
            clueManager = FindObjectOfType<ClueManager>();
            
            clueManager.OnChangeSpecialPaper += Change;
        }

        [ContextMenu("TestChange")]
        private void Change()
        {
            //SpecialPaperCanvasObject = ToChangedPaper;
        }

        private void OnDestroy()
        {
            clueManager.OnChangeSpecialPaper -= Change;
        }
    }
}
