using UnityEngine;
using UnityEngine.UI;

namespace TT
{
    public class SceneSwitchBtn : MonoBehaviour
    {
        public string defaultSceneName; 
        public string exorcismSceneName = "Exorcism";
        
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                if (PlayerPrefs.HasKey("HasReachedExorcismScene") && PlayerPrefs.GetInt("HasReachedExorcismScene") == 1)
                {
                    FindObjectOfType<SceneSwitchManager>().ChangeScene(exorcismSceneName);
                }
                else
                {
                    FindObjectOfType<SceneSwitchManager>().ChangeScene(defaultSceneName);
                }
            });
        }
    }
}