using UnityEngine;
using UnityEngine.UI;

namespace TT
{
    public class SceneSwitchBtn : MonoBehaviour
    {
        public string sceneName;
        
        private void Start()
        {
                GetComponent<Button>().onClick.AddListener(() =>
                {
                    FindObjectOfType<SceneSwitchManager>().ChangeScene(sceneName);
                });
        }
    }
}
