using UnityEngine;

namespace TT
{
    public class PickUpController : BaseController<IPickupable>
    {
        private SpecialPaperHandler specialPaperHandler;
        private InventoryUIHandler _inventoryUIHandler;

        private void Start()
        {
            specialPaperHandler = FindObjectOfType<SpecialPaperHandler>();
            _inventoryUIHandler = FindObjectOfType<InventoryUIHandler>();
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
                        TryPickUpItem(pickupable, _inventoryUIHandler.TryAddInventoryItem);
                        break;
                    case ItemType.Normal:
                        TryPickUpItem(pickupable);
                        break;
                }
                MainGameSoundManager.Instance.PlaySFX("Click_1");
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
