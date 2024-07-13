using UnityEngine;
using UnityEngine.Events;

namespace TT
{
    public class InteractableObject : MonoBehaviour, IInteractable
    {
        public UnityEvent OnInteractionEvent;
        public float InteractionCooldown = 1.0f; 
        private float lastInteractionTime;

        public virtual void Interact()
        {
            if (Time.time >= lastInteractionTime + InteractionCooldown)
            {
                OnInteractionEvent?.Invoke();
                lastInteractionTime = Time.time;
            }
        }
    }
}