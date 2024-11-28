using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace TT
{
    public class ExorcismManager : BaseManager
    {
        public static ExorcismManager Instance;
        public ExorcismZone _exorcismZone;

        // �� ����
        public int placedCandleCount = 0; 
        public int litCandleCount = 0;
        public bool hasPlacedCross = false;
        public bool hasPlacedExorcismBook = false;
        private const int totalCandleCount = 3;

        public Image fadeIamge;
        public GameObject Timeline_E;
        public GameObject ai;
        public GameObject ai_cut;


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            MainGameSoundManager.Instance.PlaySFX("BGM_GoOutSide");
        }

        /// ���� ����
        private void CheckGameClear()
        {
            if (placedCandleCount >= totalCandleCount &&
                litCandleCount >= totalCandleCount &&
                hasPlacedCross && hasPlacedExorcismBook)
            {
                Debug.Log("GameClear");
                _exorcismZone.ResetPlayerEquipment();
                StartCoroutine(GameClearTimeLine());
            }
        }

        /// �� ����
        public void PlaceCandle()
        {
            placedCandleCount++;
            CheckGameClear();
        }


        public void LightCandle()
        {
            litCandleCount++;
            Debug.Log("litCandleCount : " + litCandleCount);
            CheckGameClear();
        }

        public void PlaceCross()
        {
            hasPlacedCross = true;
            CheckGameClear();
        }

        public void PlaceExorcismBook()
        {
            hasPlacedExorcismBook = true;
            CheckGameClear();
        }

        /// ���� ���� ��ƾ
        IEnumerator GameClearTimeLine()
        {
            ai.SetActive(false);
            ai_cut.SetActive(false);
            StartCoroutine(FadeOut(0.01f));

            yield return new WaitForSeconds(1.5f);
            StartCoroutine(FadeIn(0.01f));

            yield return new WaitForSeconds(1f);
            Timeline_E.SetActive(true);

            yield return new WaitForSeconds(9f);
            StartCoroutine(FadeOut(0.005f));

            yield return new WaitForSeconds(5f);
            GameObject.Find("SceneSwitchManager").GetComponent<SceneSwitchManager>().ChangeScene("MainMenu");
        }

        /// ���̵���
        IEnumerator FadeIn(float time)
        {
            float fadeCount = 1;

            while (fadeCount > 0.001f)
            {
                fadeCount -= 0.05f;
                yield return new WaitForSeconds(time);

                fadeIamge.color = new Color(0, 0, 0, fadeCount);
            }
        }

        /// ���̵���
        IEnumerator FadeOut(float time)
        {
            float fadeCount = 0;
            while (fadeCount < 1.0f)
            {
                fadeCount += 0.05f;
                yield return new WaitForSeconds(time);

                fadeIamge.color = new Color(0, 0, 0, fadeCount);
            }
        }
    }
}