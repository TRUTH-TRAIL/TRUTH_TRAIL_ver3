using System.Collections;
using TMPro;
using UnityEngine;

namespace TT
{
    public class ExplainUITextHelper : MonoBehaviour
    {
        public TextMeshProUGUI explanationText;          
        public CanvasGroup canvasGroup;       
        public float displayDuration = 3f;    
        public float fadeDuration = 1f;
        
        private string cachedText;
           
        public void Help(string _helpText)
        {
            if (_helpText != null)
            {
                if (_helpText != cachedText)
                {
                    cachedText = _helpText;
                    
                    explanationText.text = _helpText;
                    StartCoroutine(DisplayAndFadeText());
                }
            }
        }
        
        private IEnumerator DisplayAndFadeText()
        {
            canvasGroup.alpha = 1f;

            yield return new WaitForSeconds(displayDuration);

            float elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                canvasGroup.alpha = 1f - Mathf.Clamp01(elapsedTime / fadeDuration);
                yield return null;
            }

            canvasGroup.alpha = 0f;
        }
    }
}