using System.Collections;
using UnityEngine;

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
            Light.SetActive(false);
            CursorObject.SetActive(false);

            StartCoroutine(DeathCutScene());
        }

        public void NextExorcismScene()
        {
            SaveExorcismProgress.SaveProgress();
            SceneSwitchManager.Instance.ChangeScene(ExorcismSceneName);
        }


        /// »ç¸Á ÄÆ¾À
        private IEnumerator DeathCutScene()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            playerSound.StopSound();
            playerSound.PlaySound("GameOver", false);

            Vector3 killAiPosi = AI.transform.position;
            killAiPosi.y = killAiPosi.y - 3f;
            KillAI.transform.position = killAiPosi;
            KillAI.transform.LookAt(player.transform);
            killAiPosi.y = killAiPosi.y + 3f;
            MainCemera.transform.LookAt(killAiPosi);
            
            yield return new WaitForSeconds(3f);
            Light.SetActive(true);
            KillAI.SetActive(true);
            player.Dead();
            
            yield return new WaitForSeconds(5f);
            StartCoroutine(FadeInCanvas());
        }
        
        private IEnumerator FadeInCanvas()
        {
            float elapsedTime = 0f;

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