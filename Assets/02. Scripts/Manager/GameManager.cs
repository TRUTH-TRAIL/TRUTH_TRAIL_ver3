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
        
        /// AI »ç¸Á
        public void GameOver()
        {
            AI.SetActive(false);
            Light.SetActive(false);     // ¼ÕÀüµîÀÌ Åö ²¨Áö´Â°Å ¸»°í Ä¡Ä¡Á÷ ±ôºý±ôºýÇÏ¸ç ²¨Áö´Â ¿¬Ãâ Ãß°¡
            CursorObject.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            playerSound.StopSound();
            playerSound.PlaySound("GameOver", false);
            player.Dead();

            StartCoroutine(DeathCutScene());    // »ç¸ÁÄÆ¾À ¿¬Ãâ¿ë ÄÚ·çÆ¾(½Ã³×¸Ó½Å Ä«¸Þ¶ó ºÙ¿©Çá ÇÒµí)
        }

        /// ÀúÁÖ »ç¸Á
        public void CurseGameOver()
        {
            AI.SetActive(false);
            Light.SetActive(false);     // ¼ÕÀüµîÀÌ Åö ²¨Áö´Â°Å ¸»°í Ä¡Ä¡Á÷ ±ôºý±ôºýÇÏ¸ç ²¨Áö´Â ¿¬Ãâ Ãß°¡
            CursorObject.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            playerSound.StopSound();
            playerSound.PlaySound("GameOver", false);
            player.Dead();

            StartCoroutine(FadeInCanvas(curseOverCanvasGroup));
        }

        /// Åð¸¶¾À ÀÌµ¿
        public void NextExorcismScene()
        {
            SaveExorcismProgress.SaveProgress();

            // ¾ÆÀÌÅÛ ´Ù ¸Ô¾ú´ÂÁö È®ÀÎ
            if (player.GetComponent<InventoryItemHandler>().InventoryItemUIElements.Count < 8)
            {
                //Debug.Log(player.GetComponent<InventoryItemHandler>().InventoryItemUIElements.Count);
                MainGameSoundManager.Instance.PlaySFX("SFX_LockedDoor");
                StartCoroutine(DisplayLog());
            }   
            else
            {
                // ¿­¼è ÀåÂø
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
            adviceLabel.text = "¾ÆÁ÷ ½ÀµæÇÏÁö ¾ÊÀº ¾ÆÀÌÅÛÀÌ ÀÖ½À´Ï´Ù \n ´Ü¼­¸¦ Âü°íÇÏ¿© Ã£¾Æº¸¼¼¿ä";
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


        /// »ç¸Á ÄÆ¾À
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