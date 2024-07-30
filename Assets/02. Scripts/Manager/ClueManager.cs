using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TT
{
    public class ClueManager : Singleton<ClueManager>
    {
        public List<string> realClues = new List<string>();
        public List<string> fakeClues = new List<string>();
        public List<string> curseClues = new List<string>();
        
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
        
        [Header("Clues Value")]
        public int MaxRealClues = 10;
        public int CurrentRealCluesCount;
        
        public IPickupable CurrentCurse { get; set; }
        public List<FoldedNote> Clues = new List<FoldedNote>();

        private int realClueCount = 0;
        private GameObject currentCurseUI;
        
        public Action OnChangeSpecialPaper;

        public void GetRealClue()
        {
            CurrentRealCluesCount++;
            if (CurrentRealCluesCount == 10)
            {
                OnChangeSpecialPaper?.Invoke();
            }
        }
        
        public bool CanAddRealClue()
        {
            return realClueCount < MaxRealClues;
        }

        public void AddRealClue()
        {
            if (realClueCount < MaxRealClues)
            {
                realClueCount++;
            }
        }

        public string GetClueDescription(ClueType clueType)
        {
            List<string> clueList = GetClueList(clueType);

            if (clueList.Count > 0)
            {
                string description = clueList[0];
                clueList.RemoveAt(0); 
                return description;
            }

            return null;
        }

        private List<string> GetClueList(ClueType clueType)
        {
            return clueType switch
            {
                ClueType.Real => realClues,
                ClueType.Fake => fakeClues,
                ClueType.Curse => curseClues,
                _ => new List<string>(),
            };
        }
        
        public IEnumerator DisplayLog(string message)
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

        public void AddClue(FoldedNote foldedNote)
        {
            Clues.Add(foldedNote);
            UpdateClueUI();
        }

        public void Decrypt(List<FoldedNote> list)
        {
            Clues.Clear();
            Clues = list;
            CurrentCurse = null;
            UpdateClueUI();
        }

        private void UpdateClueUI()
        {
            int upperIndex = 0;

            // 현재 부모 안에 있는 자식 요소들을 리스트로 복사
            List<Transform> upperChildren = new List<Transform>();
            foreach (Transform child in UpperGroupParent)
            {
                upperChildren.Add(child);
            }

            List<Transform> lowerChildren = new List<Transform>();
            foreach (Transform child in LowerGroupParent)
            {
                lowerChildren.Add(child);
            }

            for (int i = 0; i < Clues.Count; i++)
            {
                FoldedNote item = Clues[i];
                Transform clueUI;

                if (item.ClueType != ClueType.Curse)
                {
                    if (upperIndex < upperChildren.Count)
                    {
                        clueUI = upperChildren[upperIndex];
                        upperChildren.RemoveAt(upperIndex); // 사용된 자식 요소는 리스트에서 제거
                    }
                    else
                    {
                        clueUI = Instantiate(ClueUIPrefab, UpperGroupParent).transform;
                        clueUI.transform.localScale = Vector3.one;
                    }

                    clueUI.GetComponentInChildren<TextMeshProUGUI>().text = item.GetDescription();

                    RectTransform rectTransform = clueUI.GetComponent<RectTransform>();
                    rectTransform.anchoredPosition = new Vector2(UpperOffset.x,
                        UpperOffset.y - (upperIndex * (rectTransform.sizeDelta.y + UpperSpacing)));

                    upperIndex++;
                }
                else
                {
                    CurrentCurse = item;
                    if (lowerChildren.Count > 0)
                    {
                        currentCurseUI = lowerChildren[0].gameObject;
                        lowerChildren.RemoveAt(0); // 사용된 자식 요소는 리스트에서 제거
                    }
                    else
                    {
                        currentCurseUI = Instantiate(CurseUIPrefab, LowerGroupParent);
                        currentCurseUI.transform.localScale = Vector3.one;
                    }

                    var curseNote = CurrentCurse as FoldedNote;
                    currentCurseUI.GetComponentInChildren<TextMeshProUGUI>().text = curseNote.GetDescription();
                    currentCurseUI.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;

                    RectTransform rectTransform = currentCurseUI.GetComponent<RectTransform>();
                    rectTransform.anchoredPosition = new Vector2(LowerOffset.x, LowerOffset.y);
                }
            }

            // 사용되지 않은 UpperGroupParent 자식 요소 파괴
            foreach (var child in upperChildren)
            {
                Destroy(child.gameObject);
            }

            // 사용되지 않은 LowerGroupParent 자식 요소 파괴
            foreach (var child in lowerChildren)
            {
                Destroy(child.gameObject);
            }

            if (CurrentCurse == null && currentCurseUI != null)
            {
                Destroy(currentCurseUI);
            }
        }

    }
}