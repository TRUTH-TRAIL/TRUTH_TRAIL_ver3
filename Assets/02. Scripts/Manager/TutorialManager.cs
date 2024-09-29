using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TT
{
    public class TutorialManager : BaseManager
    {
        public AudioSource horrorSound;
        public AudioSource attackSound;
        public GameObject flashlight;
        public float flashDuration = 0.5f; 
        public int flashCount = 4;

        public int count;
        
        public string mainSceneName = "MainGame"; 
        public CanvasGroup blackScreenCanvasGroup;
        
        private void Start()
        {
            count = 0;
        }
        
        public void ClearTutorial()
        {
            if (count == 3)
            {
                StartCoroutine(TutorialSequence());
            }
        }

        public void AddCount()
        {
            count += 1;
        }
        
        private IEnumerator TutorialSequence()
        {
            horrorSound.Play();

            for (int i = 0; i < flashCount; i++)
            {
                flashlight.SetActive(false);
                yield return new WaitForSeconds(flashDuration);
                flashlight.SetActive(true);
                yield return new WaitForSeconds(flashDuration);
            }

            attackSound.Play();
            yield return StartCoroutine(TransitionToBlackScreen());
        }

        private IEnumerator TransitionToBlackScreen()
        {
            blackScreenCanvasGroup.alpha = 0f;

            float fadeDuration = 1f;
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                blackScreenCanvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
                yield return null;
            }

            blackScreenCanvasGroup.alpha = 1f;

            yield return new WaitForSeconds(0.5f);
            
            SceneManager.LoadSceneAsync(mainSceneName);
        }
    }
}
