using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TT
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public GameObject AI;
        public GameObject KillAI;
        public GameObject Light;
        public GameObject CursorObject;
        
        public CanvasGroup gameOverCanvasGroup; 

        public float fadeDuration = 1.0f;

        public string CurrentSceneName;
        public string MenuSceneName;

        private Player Player;
        private void Awake()
        {
            Player = FindObjectOfType<Player>();
            Instance = this;
        }

        private void Start()
        {
            Light = GameObject.FindGameObjectWithTag("Lights");
            CursorObject = GameObject.FindGameObjectWithTag("InteractionCanvas");
        }

        public void ReGame()
        {
            Player.IsDead = false;
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
            KillAI.SetActive(true);
            Player.Dead();

            var o = FindObjectOfType<PlayerSound>();
            o.StopSound();
            o.PlaySound("GameOver", false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
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