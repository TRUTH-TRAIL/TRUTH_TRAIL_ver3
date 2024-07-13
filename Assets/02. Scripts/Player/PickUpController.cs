using UnityEngine;

namespace TT
{
    public class PickUpController : MonoBehaviour
    {
        public float PickupRange = 2.0f;
        public GameObject PickupText;
        public ClueInventory ClueInventory;

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
            Collider collider = RaycastUtil.TryGetPickupableCollider(cam, PickupRange);
            if (collider != null)
            {
                IPickupable pickupable = collider.GetComponent<IPickupable>();
                if (pickupable != null)
                {
                    ShowPickupText(pickupable);
                    if (Input.GetMouseButtonDown(0))
                    {
                        TryPickUpClue(pickupable);
                    }
                }
                else
                {
                    HidePickupText();
                }
            }
            else
            {
                HidePickupText();
            }
        }

        private void ShowPickupText(IPickupable pickupable)
        {
            PickupText.SetActive(true);
        }

        private void HidePickupText()
        {
            PickupText.SetActive(false);
        }

        private void TryPickUpClue(IPickupable pickupable)
        {
            ClueObject clueObject = pickupable as ClueObject;
            if (clueObject != null && ClueInventory.TryAddClue(clueObject.Clue))
            {
                pickupable.OnPickUp();
            }
        }
    }
}
