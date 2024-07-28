using UnityEngine;
using UnityEngine.Serialization;

namespace TT
{
    public class PickUpController : MonoBehaviour
    {
        public float PickupRange = 2.0f;
        public GameObject PickupText;
        
        [FormerlySerializedAs("ClueInventory")] 
        public SpecialPaperController specialPaperController;
        public InventoryController inventoryController;
        public LayerMask PickupLayerMask;
        
        private Camera cam;

        private void Awake()
        {
            cam = Camera.main;
        }

        private void Update()
        {
            CheckForPickupable();
        }

        private void CheckForPickupable()
        {
            Collider collider = RaycastUtil.TryGetPickupableCollider(cam, PickupRange, PickupLayerMask);
            if (collider != null)
            {
                IPickupable pickupable = collider.GetComponent<IPickupable>();
                if (pickupable != null)
                {
                    HandlePickup(pickupable);
                }
                else
                {
                    SetPickupTextActive(false);
                }
            }
            else
            {
                SetPickupTextActive(false);
            }
        }

        private void HandlePickup(IPickupable pickupable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                switch (pickupable.GetItemType())
                {
                    case ItemType.Clue:
                        TryPickUpItem(pickupable, specialPaperController.TryAddClue);
                        break;
                    case ItemType.InventoryItem:
                        TryPickUpItem(pickupable, inventoryController.TryAddInventoryItem);
                        break;
                }
            }
            else
            {
                SetPickupTextActive(true);
            }
        }

        private void TryPickUpItem(IPickupable pickupable, System.Func<IPickupable, bool> tryAddMethod)
        {
            PickupableObject pickupableObject = pickupable as PickupableObject;
            if (pickupableObject != null && tryAddMethod(pickupableObject))
            {
                pickupable.OnPickUp();
                SetPickupTextActive(false);
            }
        }

        private void SetPickupTextActive(bool isActive)
        {
            PickupText.SetActive(isActive);
        }
    }
}
