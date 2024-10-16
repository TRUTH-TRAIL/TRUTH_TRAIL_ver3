using UnityEngine;
using UnityEngine.Events;

namespace TT
{
    public class PickupableObject : MonoBehaviour, IPickupable
    {
        public Item item;
        public UnityEvent OnPickUpEvent;
        
        protected virtual void Awake()
        {
            gameObject.layer = LayerMask.NameToLayer("Pickupable");
        }
        
        public void OnPickUp()
        {
            OnPickUpEvent?.Invoke();
            gameObject.SetActive(false);
        }

        public ItemType GetItemType()
        {
            return item.GetItemType();
        }
    }
}
