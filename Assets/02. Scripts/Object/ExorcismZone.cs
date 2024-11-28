using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TT
{
    public class ExorcismZone : MonoBehaviour, IInteractable
    {
        // 플레이어 좌클릭 검사 -> is exorxism zone -> 장착 아이템명 return 
        // -> 아이템명에 따른 activatedObjects Active
        // 라이터는 양초에 클릭 추가 검사
        public List<GameObject> activatedObjects;

        [HideInInspector] public InventoryUIHandler inventoryUIHandler;
        [HideInInspector] public InventoryItemHandler inventoryItemHandler;
       
        private bool isPlayerInZone = false;
        private bool isJustOnce;        

        
        private void Awake()
        {
            inventoryUIHandler = FindObjectOfType<InventoryUIHandler>();
            inventoryItemHandler = FindObjectOfType<InventoryItemHandler>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Interact();
            }
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

        /// 아이템 배치 시도
        public void Interact()
        {
            if (isPlayerInZone)
            {
                TryPlaceObject();
            }
        }

        /// 퇴마 아이템 배치
        private void TryPlaceObject()
        {
            // 장착한 아이템명 get
            string itemName = GetEqiupItemName();
            Debug.Log("itemName : " + itemName);

            // 배치 로직 작성
            switch (itemName)
            {
                case "SpecialCandle1":
                    activatedObjects[0].SetActive(true);
                    ExorcismManager.Instance.PlaceCandle();
                    inventoryUIHandler.RemoveInventoryItem(itemName);
                    ResetPlayerEquipment();
                    break;

                case "SpecialCandle2":
                    activatedObjects[1].SetActive(true);
                    ExorcismManager.Instance.PlaceCandle();
                    inventoryUIHandler.RemoveInventoryItem(itemName);
                    ResetPlayerEquipment();
                    break;

                case "SpecialCandle3":
                    activatedObjects[2].SetActive(true);
                    ExorcismManager.Instance.PlaceCandle();
                    inventoryUIHandler.RemoveInventoryItem(itemName);
                    ResetPlayerEquipment();
                    break;

                case "Cross":
                    activatedObjects[3].SetActive(true);
                    ExorcismManager.Instance.PlaceCross();
                    inventoryUIHandler.RemoveInventoryItem(itemName);
                    ResetPlayerEquipment();
                    break;

                case "Lighter":
                    Ray ray = new Ray(Player.Instance.transform.position, Player.Instance.transform.forward);
                    if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Candle")))
                    {
                        Transform fireTransform = hit.collider.gameObject.transform.Find("Fire");

                        if (fireTransform != null)
                        {
                            fireTransform.gameObject.SetActive(true);
                            ExorcismManager.Instance.LightCandle();
                            hit.collider.enabled = false;
                            // 라이터 사운드 여기
                        }
                        else
                        {
                            Debug.LogWarning("Fire 오브젝트를 찾을 수 없음");
                        }
                    }
                    break;

                case "SpecialPaper":
                    activatedObjects[4].SetActive(true);
                    ExorcismManager.Instance.PlaceExorcismBook();
                    inventoryUIHandler.RemoveInventoryItem(itemName);
                    ResetPlayerEquipment();
                    break;
            }

            
        }

        /// 장착 아이템 검사
        private string GetEqiupItemName()
        {
            string item = null;

            if (Player.Instance.isEqiupSpecialCandle1)
                item = "SpecialCandle1";
            else if(Player.Instance.isEqiupSpecialCandle2)
                item = "SpecialCandle2";
            else if (Player.Instance.isEqiupSpecialCandle3)
                item = "SpecialCandle3";
            else if (Player.Instance.isEqiupCross)
                item = "Cross";
            else if (Player.Instance.isEqiupLighter)
                item = "Lighter";
            else if (Player.Instance.isEqiupSpecialPaper)
                item = "SpecialPaper";

            return item;
        }
      
        /// 아이템 장착상태 해제
        public void ResetPlayerEquipment()
        {
            Player.Instance.isEqiupSpecialCandle1 = false;
            Player.Instance.isEqiupSpecialCandle2 = false;
            Player.Instance.isEqiupSpecialCandle3 = false;
            Player.Instance.isEqiupLighter = false;
            Player.Instance.isEqiupCross = false;
            Player.Instance.isEqiupSpecialPaper = false;
            Player.Instance.isEqiupKey = false;

            inventoryItemHandler.ChangeEquippedItem(null, null, true);
        }
    }
}
