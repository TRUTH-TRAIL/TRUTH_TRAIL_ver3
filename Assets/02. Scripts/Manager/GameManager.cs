using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
        public float fadeDuration = 1.0f;

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

        public void NextExorcismScene()
        {
            SaveExorcismProgress.SaveProgress();
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


        /// »ç¸Á ÄÆ¾À
        private IEnumerator DeathCutScene()
        {
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
            StartCoroutine(FadeInCanvas());
        }
        
        private IEnumerator FadeInCanvas()
        {
            float elapsedTime = 0f;
            Light.SetActive(false);

            gameOverCanvasGroup.alpha = 0f;
            gameOverCanvasGroup.gameObject.SetActive(true);

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                gameOverCanvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
                yield return null;
            }

            gameOverCanvasGroup.interactable = true;
            gameOverCanvasGroup.blocksRaycasts = true;
            gameOverCanvasGroup.alpha = 1f;
        } 
    }
}