using System;
using UnityEngine;

namespace TT
{
    public class PickUpController : BaseController<IPickupable>
    {
        private SpecialPaperHandler specialPaperHandler;
        private InventoryHandler inventoryHandler;

        private void OnDrawGizmos()
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
                }
            }
            else
            {
                InteractionTextUI.Instance.SetPickupTextActive(true, BaseString);
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
