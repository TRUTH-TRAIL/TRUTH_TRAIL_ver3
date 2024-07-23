using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TT
{
    public class SpecialPaperController : MonoBehaviour
    {
        [Header("Clue Count")] 
        public int MaxClues = 10;

        [Header("UI")] 
        public Transform UpperGroupParent;
        public Transform LowerGroupParent;
        public TextMeshProUGUI LogText;
        public float ShowLogTime = 2f;

        [Header("생성되는 UI Prefab")]
        public GameObject ClueUIPrefab;
        public GameObject CurseUIPrefab;
        
        [Header("Offset")]
        public Vector2 UpperOffset;
        public Vector2 LowerOffset;
        public float UpperSpacing;

        [Header("습득물보관(특수용지)")] 
        public GameObject ClueInventoryObject;
        public KeyCode OpenInventoryKeyCode = KeyCode.Tab;

        [Header("장착 시")] 
        public bool IsEquipped;
        public GameObject SpecialPaperImage;
        public Vector2 EquippedPosition = new Vector3(491.600006f,-508.589996f,0);
        public Quaternion EquippedRotation = Quaternion.Euler(0,0,27.3699989f);
        private Vector2 originalPosition = new Vector2(0,0);
        private Quaternion originalRotation = Quaternion.Euler(0,0,0);
        
        private bool isActiveState = false;
        private List<Item> clues = new List<Item>();
        private Item currentCurse;

        public bool TryAddClue(Item item)
        {
            if (item == null) return false;
            
            if (item.GetClueType() == ClueType.Curse)
            {
                currentCurse = item;
            }
            else if (clues.Count >= MaxClues)
            {
                StartCoroutine(DisplayLog("Cannot pick up more clues. Inventory is full."));
                return false;
            }

            clues.Add(item);
            UpdateClueUI();
            return true;
        }

        private IEnumerator DisplayLog(string message)
        {
            LogText.text = message;
            LogText.gameObject.SetActive(true);

            Color originalColor = LogText.color;
            yield return new WaitForSeconds(ShowLogTime);

            for (float t = 0.5f; t > 0; t -= Time.deltaTime)
            {
                Color newColor = originalColor;
                newColor.a = t / 0.5f;
                LogText.color = newColor;
                yield return null;
            }

            LogText.gameObject.SetActive(false);
            LogText.color = originalColor;
        }

        public List<Item> GetClues() => clues;

        private void UpdateClueUI()
        {
            for (int i = 0; i < clues.Count; i++)
            {
                Item item = clues[i];
                if (item.GetClueType() == ClueType.Curse) continue;

                Transform clueUI;
                if (i < UpperGroupParent.childCount)
                {
                    clueUI = UpperGroupParent.GetChild(i);
                }
                else
                {
                    clueUI = Instantiate(ClueUIPrefab, UpperGroupParent).transform;
                }

                clueUI.GetComponentInChildren<TextMeshProUGUI>().text = item.GetDescription();
                clueUI.GetComponentInChildren<Image>().sprite = item.GetImage();

                RectTransform rectTransform = clueUI.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(UpperOffset.x, UpperOffset.y - (i * (rectTransform.sizeDelta.y + UpperSpacing)));

                clueUI.gameObject.SetActive(true);
            }

            for (int i = clues.Count; i < UpperGroupParent.childCount; i++)
            {
                UpperGroupParent.GetChild(i).gameObject.SetActive(false);
            }

            if (currentCurse != null)
            {
                Transform curseUI;
                if (LowerGroupParent.childCount > 0)
                {
                    curseUI = LowerGroupParent.GetChild(0);
                }
                else
                {
                    curseUI = Instantiate(CurseUIPrefab, LowerGroupParent).transform;
                }

                curseUI.GetComponentInChildren<TextMeshProUGUI>().text = currentCurse.GetDescription();
                curseUI.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
                curseUI.GetComponentInChildren<Image>().sprite = currentCurse.GetImage();

                RectTransform rectTransform = curseUI.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(LowerOffset.x, LowerOffset.y);

                curseUI.gameObject.SetActive(true);
            }

            for (int i = (currentCurse != null ? 1 : 0); i < LowerGroupParent.childCount; i++)
            {
                LowerGroupParent.GetChild(i).gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (Input.GetKey(OpenInventoryKeyCode))
            {
                if (!ClueInventoryObject.activeSelf && !IsEquipped)
                {
                    ClueInventoryObject.SetActive(true);
                }
            }
            else if (Input.GetKeyUp(OpenInventoryKeyCode))
            {
                ClueInventoryObject.SetActive(false);
            }

            SpecialPaperImage.transform.localPosition = IsEquipped ? EquippedPosition : originalPosition;
            SpecialPaperImage.transform.localRotation = IsEquipped ? EquippedRotation : originalRotation;
        }
    }
}
