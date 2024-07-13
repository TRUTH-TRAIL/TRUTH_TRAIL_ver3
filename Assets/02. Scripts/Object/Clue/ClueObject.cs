using UnityEngine;
using UnityEngine.Events;

namespace TT
{
    public class ClueObject : MonoBehaviour, IPickupable
    {
        public Clue Clue;

        public string GetDescription() => Clue.GetDescription();
        public ClueType GetClueType() => Clue.GetClueType();
        public Sprite GetImage() => Clue.GetImage();

        public UnityEvent OnPickUpEvent;
        
        public void OnPickUp()
        {
            OnPickUpEvent?.Invoke();
            Destroy(gameObject);
        }
    }
}
