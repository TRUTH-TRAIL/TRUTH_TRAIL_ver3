using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

namespace TT
{
    public class ExorcismManager : BaseManager
    {
        public static ExorcismManager Instance;
        public ExorcismZone _exorcismZone;
        public PlayerSound _playerSound;

        // 퇴마 조건
        public int placedCandleCount = 0; 
        public int litCandleCount = 0;
        public bool hasPlacedCross = false;
        public bool hasPlacedExorcismBook = false;
        private const int totalCandleCount = 3;

        public GameObject cursor;
        public TextMeshProUGUI adviceLabel;
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

        /// 게임 성공
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

        /// 퇴마 조건
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

        /// 게임 성공 루틴
        IEnumerator GameClearTimeLine()
        {
            MainGameSoundManager.Instance.StopBGM();
            MainGameSoundManager.Instance.StopSFX();
            _playerSound.StopSound();

            ai.SetActive(false);
            ai_cut.SetActive(false);
            cursor.SetActive(false);
            StartCoroutine(FadeOut(0.01f));

            yield return new WaitForSeconds(1.5f);
            StartCoroutine(FadeIn(0.01f));

            Timeline_E.SetActive(true);
            yield return new WaitForSeconds(1f);
            MainGameSoundManager.Instance.PlaySFX("BGM_Exorcism");

            yield return new WaitForSeconds(15f);
            StartCoroutine(FadeOut(0.005f));

            yield return new WaitForSeconds(3f);
            StartCoroutine(SuccessLabelActive());
            yield return new WaitForSeconds(7f);
            StartCoroutine(SuccessLabelDActive());
            yield return new WaitForSeconds(3f);

            // 기록 초기화
            PlayerPrefs.SetString("LastScene", "MainGame");
            PlayerPrefs.Save();

            GameObject.Find("SceneSwitchManager").GetComponent<SceneSwitchManager>().ChangeScene("MainMenu");
        }

        /// 페이드인
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

        /// 페이드아웃
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

        IEnumerator SuccessLabelActive()
        {
            float fadeCount = 0;
            while (fadeCount < 1.0f)
            {
                fadeCount += 0.05f;
                yield return new WaitForSeconds(0.01f);

                adviceLabel.color = new Color(205, 198, 198, fadeCount);
            }
        }

        IEnumerator SuccessLabelDActive()
        {
            float fadeCount = 1f;
            while (fadeCount > 0.001f)
            {
                fadeCount -= 0.05f;
                yield return new WaitForSeconds(0.01f);

                adviceLabel.color = new Color(205, 198, 198, fadeCount);
            }
        }
    }
}
