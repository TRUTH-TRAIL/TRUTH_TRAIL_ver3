using UnityEngine;

namespace TT
{
    public class CanvasToggle : MonoBehaviour
    {
        public CanvasGroup CanvasGroup;

        public bool IsActive { get; set; }

        public void Toggle()
        {
            IsActive = !IsActive;
            CanvasGroup.alpha = IsActive ? 1f : 0f;
            CanvasGroup.interactable = IsActive;
            CanvasGroup.blocksRaycasts = IsActive;
        }

        public void Off()
        {
            IsActive = false;
            CanvasGroup.alpha = IsActive ? 1f : 0f;
            CanvasGroup.interactable = IsActive;
            CanvasGroup.blocksRaycasts = IsActive;
        }
    }
}
