using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TT
{
    public class ContinueGame : MonoBehaviour
    {
        public void Continue()
        {
            string lastScene = PlayerPrefs.GetString("LastScene", "MainGame");
            SceneManager.LoadScene(lastScene);
        }
    }
}
