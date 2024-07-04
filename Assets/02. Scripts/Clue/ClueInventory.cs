using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TT
{
    public class ClueInventory : MonoBehaviour
    {
        [Header("Clue Count")] public int MaxClues = 10;

        [Header("UI")] public Transform UpperGroupParent;
        public Transform LowerGroupParent;
        public GameObject ClueUIPrefab;
        public GameObject CurseUIPrefab;
        public TextMeshProUGUI LogText;
        public float ShowLogTime = 2f;

        public Vector2 UpperOffset;
        public Vector2 LowerOffset;
        public float UpperSpacing;

        public GameObject SpecialPaper;
        public bool IsOn { get; set; }

        [Header("KeyCode")] public KeyCode OpenSpecialPaperKey = KeyCode.R;

        private List<Clue> clues = new List<Clue>();
        private Clue currentCurse;

        public bool TryAddClue(Clue clue)
        {
            if (clue.GetClueType() == ClueType.Curse)
            {
                currentCurse = clue;
            }
            else if (clues.Count >= MaxClues)
            {
                StartCoroutine(DisplayLog("Cannot pick up more clues. Inventory is full."));
                return false;
            }

            clues.Add(clue);
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

        public List<Clue> GetClues() => clues;

        private void UpdateClueUI()
        {
            for (int i = 0; i < clues.Count; i++)
            {
                Clue clue = clues[i];
                if (clue.GetClueType() == ClueType.Curse) continue;

                Transform clueUI;
                if (i < UpperGroupParent.childCount)
                {
                    clueUI = UpperGroupParent.GetChild(i);
                }
                else
                {
                    clueUI = Instantiate(ClueUIPrefab, UpperGroupParent).transform;
                }

                clueUI.GetComponentInChildren<TextMeshProUGUI>().text = clue.GetDescription();
                clueUI.GetComponentInChildren<Image>().sprite = clue.GetImage();

                RectTransform rectTransform = clueUI.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(UpperOffset.x,
                    UpperOffset.y - (i * (rectTransform.sizeDelta.y + UpperSpacing)));

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
            if (Input.GetKeyDown(OpenSpecialPaperKey))
            {
                IsOn = !IsOn;
                SpecialPaper.SetActive(IsOn);
            }
        }
    }
}
