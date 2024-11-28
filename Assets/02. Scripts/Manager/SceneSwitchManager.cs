
using UnityEngine.SceneManagement;

namespace TT
{
    public class SceneSwitchManager : Singleton<SceneSwitchManager>
    {
        public void ChangeScene(string sceneName)
        {
            print(sceneName);
            SceneManager.LoadSceneAsync(sceneName);
            
        }
    }
}
