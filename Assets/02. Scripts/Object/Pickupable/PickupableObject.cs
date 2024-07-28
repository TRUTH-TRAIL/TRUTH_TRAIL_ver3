using UnityEngine;
using UnityEngine.Events;

namespace TT
{
    public class PickupableObject : MonoBehaviour, IPickupable
    {
        public Item item;
        public ItemType GetItemType() => item.GetItemType();
        
        public UnityEvent OnPickUpEvent;
        
        protected virtual void Awake()
        {
            gameObject.layer = LayerMask.NameToLayer("Pickupable");
        }
        
        public void OnPickUp()
        {
            OnPickUpEvent?.Invoke();
            Destroy(gameObject);
        }

    }
}
