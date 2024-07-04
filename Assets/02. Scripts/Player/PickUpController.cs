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
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, PickupRange))
            {
                IPickupable pickupable = hit.collider.GetComponent<IPickupable>();
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
            //PickupText.text = $"줍기"; // {pickupable.GetDescription()}
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
