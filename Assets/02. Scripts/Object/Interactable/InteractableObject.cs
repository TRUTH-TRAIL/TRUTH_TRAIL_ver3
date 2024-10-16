using UnityEngine;
using UnityEngine.Events;

namespace TT
{
    public enum InteractionType
    {
        Book = 0,
        Drawer = 1,
        Normal = 2,
    }
    
    public class InteractableObject : MonoBehaviour, IInteractable
    {
        public UnityEvent OnInteractionEvent;
        public float InteractionCooldown = 1.0f; 
        private float lastInteractionTime;

        public bool IsNeedPrerequisites = true;

        public InteractionType InteractionType = InteractionType.Normal;
        
        protected virtual void Awake()
        {
            gameObject.layer = LayerMask.NameToLayer("Interactionable");
        }

        public virtual void Interact()
        {
            if (Time.time >= lastInteractionTime + InteractionCooldown && IsNeedPrerequisites)
            {
                OnInteractionEvent?.Invoke();
                lastInteractionTime = Time.time;
            }
        }

        public void AchievePrerequisite()
        {
            IsNeedPrerequisites = true;
        }
    }
}