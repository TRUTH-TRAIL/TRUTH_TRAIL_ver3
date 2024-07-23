using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace TT
{
    [RequireComponent(typeof(Collider))]
    public class PickupableObject : MonoBehaviour, IPickupable
    {
        [FormerlySerializedAs("Clue")] public Item item;

        public string GetDescription() => item.GetDescription();
        public ClueType GetClueType() => item.GetClueType();
        public ItemType GetItemType() => item.GetItemType();

        public Sprite GetImage() => item.GetImage();

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
