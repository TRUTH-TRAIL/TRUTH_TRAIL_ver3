
using UnityEngine.SceneManagement;

namespace TT
{
    public class SceneSwitchManager : Singleton<SceneSwitchManager>
    {
        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
