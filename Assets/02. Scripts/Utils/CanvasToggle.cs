using UnityEngine;

namespace TT
{
    public class CanvasToggle : MonoBehaviour
    {
        public CanvasGroup CanvasGroup;

        private bool isActive;
        
        public void Toggle()
        {
            isActive = !isActive;
            CanvasGroup.alpha = isActive ? 1f : 0f;
            CanvasGroup.interactable = isActive;
            CanvasGroup.blocksRaycasts = isActive;
        }
    }
}
