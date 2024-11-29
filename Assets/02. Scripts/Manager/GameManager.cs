using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TT
{
    public class GameManager : BaseManager
    {
        public static GameManager Instance;

        public GameObject AI;
        public GameObject KillAI;
        public GameObject Light;
        public GameObject CursorObject;
        public Camera MainCemera;
        
        public CanvasGroup gameOverCanvasGroup;
        public CanvasGroup curseOverCanvasGroup;
        public float fadeDuration = 1.0f;
        public TextMeshProUGUI adviceLabel;

        public string CurrentSceneName;
        public string MenuSceneName = "MainMenu";
        public string ExorcismSceneName = "Exorcism";

        private Player player;
        private PlayerSound playerSound;


        private void Awake()
        {
            player = FindObjectOfType<Player>();
            playerSound = FindObjectOfType<PlayerSound>();
            Instance = this;
        }

        private void Start()
        {
            Light ??= GameObject.FindGameObjectWithTag("Lights");
            CursorObject ??= GameObject.FindGameObjectWithTag("InteractionCanvas");

            // 라스트씬 저장
            PlayerPrefs.SetString("LastScene", CurrentSceneName);
            PlayerPrefs.Save();
        }

        public void ReGame()
        {
            player.IsDead = false;
            SceneSwitchManager.Instance.ChangeScene(CurrentSceneName);
        }

        public void GoMenu()
        {
            SceneSwitchManager.Instance.ChangeScene(MenuSceneName);
        }
        
        /// AI 사망
        public void GameOver()
        {
            AI.SetActive(false);
            Light.SetActive(false);     // 손전등이 툭 꺼지는거 말고 치치직 깜빡깜빡하며 꺼지는 연출 추가
            CursorObject.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            MainGameSoundManager.Instance.StopBGM();
            playerSound.StopSound();
            playerSound.PlaySound("GameOver", false);
            player.Dead();

            StartCoroutine(DeathCutScene());    // 사망컷씬 연출용 코루틴(시네머신 카메라 붙여햐 할듯)
        }

        /// 저주 사망
        public void CurseGameOver()
        {
            AI.SetActive(false);
            Light.SetActive(false);     // 손전등이 툭 꺼지는거 말고 치치직 깜빡깜빡하며 꺼지는 연출 추가
            CursorObject.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            playerSound.StopSound();
            playerSound.PlaySound("GameOver", false);
            player.Dead();

            StartCoroutine(FadeInCanvas(curseOverCanvasGroup));
        }

        /// 퇴마씬 이동
        public void NextExorcismScene()
        {
            SaveExorcismProgress.SaveProgress();

            // 아이템 다 먹었는지 확인
            if (player.GetComponent<InventoryItemHandler>().InventoryItemUIElements.Count < 7 || !player.isAcquiredBattery)
            {
                //Debug.Log(player.GetComponent<InventoryItemHandler>().InventoryItemUIElements.Count);
                MainGameSoundManager.Instance.PlaySFX("SFX_LockedDoor");
                StartCoroutine(DisplayLog());
            }   
            else
            {
                // 열쇠 장착
                if (player.isEqiupKey)
                {
                    MainGameSoundManager.Instance.PlaySFX("SFX_Key");
                    SceneSwitchManager.Instance.ChangeScene(ExorcismSceneName);
                }
                else
                {
                    MainGameSoundManager.Instance.PlaySFX("SFX_LockedDoor");
                }
            }

            
        }

        public IEnumerator DisplayLog()
        {
            adviceLabel.text = "아직 습득하지 않은 아이템이 있습니다 \n 단서를 참고하여 찾아보세요";
            adviceLabel.gameObject.SetActive(true);

            Color originalColor = adviceLabel.color;
            yield return new WaitForSeconds(3f);

            for (float t = 0.5f; t > 0; t -= Time.deltaTime)
            {
                Color newColor = originalColor;
                newColor.a = t / 0.5f;
                adviceLabel.color = newColor;
                yield return null;
            }

            adviceLabel.gameObject.SetActive(false);
            adviceLabel.color = originalColor;
        }


        /// 사망 컷씬
        private IEnumerator DeathCutScene()
        {
            MainGameSoundManager.Instance.AiSoundPlay("SFX_DeadCutSceneAI");

            Vector3 killAiPosi = AI.transform.position;
            killAiPosi.y = killAiPosi.y - 3f;
            KillAI.transform.position = killAiPosi;
            KillAI.transform.LookAt(player.transform);
            killAiPosi.y = killAiPosi.y + 2f;
            MainCemera.transform.LookAt(killAiPosi);
            
            yield return new WaitForSeconds(3f);
            Light.SetActive(true);
            KillAI.SetActive(true);
            MainCemera.GetComponent<CameraShake>().OnCameraShake(5f);

            yield return new WaitForSeconds(5f);
            StartCoroutine(FadeInCanvas(gameOverCanvasGroup));
        }
        
        private IEnumerator FadeInCanvas(CanvasGroup c)
        {
            float elapsedTime = 0f;
            Light.SetActive(false);

            c.alpha = 0f;
            c.gameObject.SetActive(true);

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                c.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
                yield return null;
            }

            c.interactable = true;
            c.blocksRaycasts = true;
            c.alpha = 1f;
        } 
    }
}