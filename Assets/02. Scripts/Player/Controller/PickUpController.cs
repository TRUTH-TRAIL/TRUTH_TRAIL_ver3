using UnityEngine;

namespace TT
{
    public class PickUpController : BaseController<IPickupable>
    {
        private SpecialPaperHandler specialPaperHandler;
        private InventoryHandler inventoryHandler;

        private void Start()
        {
            specialPaperHandler = FindObjectOfType<SpecialPaperHandler>();
            inventoryHandler = FindObjectOfType<InventoryHandler>();
        }

        protected override void HandleAction(IPickupable pickupable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                switch (pickupable.GetItemType())
                {
                    case ItemType.Clue:
                        TryPickUpItem(pickupable, specialPaperHandler.TryAddClue);
                        break;
                    case ItemType.InventoryItem:
                        TryPickUpItem(pickupable, inventoryHandler.TryAddInventoryItem);
                        break;
                    case ItemType.Normal:
                        TryPickUpItem(pickupable);
                        break;
                }
            }
            else
            {
                InteractionTextUI.Instance.SetPickupTextActive(true, BaseString);
            }
        }
        
        private void TryPickUpItem(IPickupable pickupable)
        {
            PickupableObject pickupableObject = pickupable as PickupableObject;
            if (pickupableObject != null)
            {
                pickupable.OnPickUp();
                InteractionTextUI.Instance.SetPickupTextActive(false, BaseString);
            }
        }
        
        private void TryPickUpItem(IPickupable pickupable, System.Func<IPickupable, bool> tryAddMethod)
        {
            PickupableObject pickupableObject = pickupable as PickupableObject;
            if (pickupableObject != null && tryAddMethod(pickupableObject))
            {
                pickupable.OnPickUp();
                InteractionTextUI.Instance.SetPickupTextActive(false, BaseString);
            }
        }
        
       
    }
}
