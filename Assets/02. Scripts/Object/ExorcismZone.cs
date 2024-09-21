using System;
using UnityEngine;

namespace TT
{
    public class ExorcismZone : MonoBehaviour, IInteractable
    {
        public string requiredItem; 
        public GameObject objectToPlace;
        public Transform placementPosition;
        public InventoryUIHandler inventoryUIHandler;  
        private bool isPlayerInZone = false;

        private void Awake()
        {
            inventoryUIHandler = FindObjectOfType<InventoryUIHandler>();
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
            if (isPlayerInZone) 
            {
                TryPlaceObject();
            }
        }
        
        private void TryPlaceObject()
        {
            if (IsPlayerHoldingRequiredItem())
            {
                PlaceObject();
                inventoryUIHandler.RemoveInventoryItem(requiredItem);
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
                case "Key":
                    return Player.Instance.isEqiupKey;
                default:
                    return false;
            }
        }

        private void PlaceObject()
        {
            if (objectToPlace != null && placementPosition != null)
            {
                objectToPlace.transform.position = placementPosition.position;
                objectToPlace.transform.rotation = placementPosition.rotation;
                objectToPlace.SetActive(true);
            }
        }
        
    }
}