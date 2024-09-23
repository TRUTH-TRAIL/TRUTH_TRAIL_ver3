using UnityEngine;

namespace TT
{
    public class ExorcismZone : MonoBehaviour, IInteractable
    {
        public string requiredItem;
        public GameObject objectToPlace;
        public Transform placementPosition;
        [HideInInspector] public InventoryUIHandler inventoryUIHandler;
        [HideInInspector] public InventoryItemHandler inventoryItemHandler;
       
        private bool isPlayerInZone = false;
        private bool isJustOnce;
        
        private void Awake()
        {
            inventoryUIHandler = FindObjectOfType<InventoryUIHandler>();
            inventoryItemHandler = FindObjectOfType<InventoryItemHandler>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerInZone = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerInZone = false;
            }
        }

        public void Interact()
        {
            if (isPlayerInZone && !isJustOnce)
            {
                TryPlaceObject();
            }
        }

        private void TryPlaceObject()
        {
            if (IsPlayerHoldingRequiredItem())
            {
                PlaceObject();
                isJustOnce = true; // TODO : UI에 이미 배치했다는 경고 ㄱ?
                inventoryUIHandler.RemoveInventoryItem(requiredItem);
                ResetPlayerEquipment();

                if (requiredItem == "SpecialCandle")
                {
                    ExorcismManager.Instance.PlaceCandle();
                }
                else if (requiredItem == "Lighter")
                {
                    ExorcismManager.Instance.LightCandle();
                }
                else if (requiredItem == "Cross")
                {
                    ExorcismManager.Instance.PlaceCross();
                }
                else if (requiredItem == "SpecialPaper")
                {
                    ExorcismManager.Instance.PlaceExorcismBook();
                }
            }
        }

        private bool IsPlayerHoldingRequiredItem()
        {
            switch (requiredItem)
            {
                case "SpecialCandle":
                    return Player.Instance.isEqiupSpecialCandle;
                case "Lighter":
                    return Player.Instance.isEqiupLighter;
                case "Cross":
                    return Player.Instance.isEqiupCross;
                case "SpecialPaper":
                    return Player.Instance.isEqiupSpecialPaper;
                default:
                    return false;
            }
        }

        private void PlaceObject()
        {
            if (objectToPlace != null && placementPosition != null)
            {
                GameObject spawnObject = Instantiate(objectToPlace);
                spawnObject.transform.position = placementPosition.position;
                spawnObject.transform.rotation = placementPosition.rotation;
                spawnObject.SetActive(true);
            }
        }

        private void ResetPlayerEquipment()
        {
            Player.Instance.isEqiupSpecialCandle = false;
            Player.Instance.isEqiupLighter = false;
            Player.Instance.isEqiupCross = false;
            Player.Instance.isEqiupSpecialPaper = false;
            Player.Instance.isEqiupKey = false;

            inventoryItemHandler.ChangeEquippedItem(null, null, true);
        }
    }
}
