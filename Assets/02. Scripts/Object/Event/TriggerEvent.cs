using UnityEngine;
using UnityEngine.Events;

namespace TT
{
    public class TriggerEvent : MonoBehaviour
    {
        public UnityEvent OnTriggerEvent = new UnityEvent();
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnTriggerEvent?.Invoke();
            }
        }
    }
}
