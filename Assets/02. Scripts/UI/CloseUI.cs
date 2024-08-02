using UnityEngine;

namespace TT
{
    public class CloseUI : MonoBehaviour
    {
        public KeyCode CloseKeyCode = KeyCode.Escape;
        
        private void Update()
        {
            if (gameObject.activeSelf)
            {
                if (Input.GetKeyDown(CloseKeyCode) || Input.GetMouseButtonDown(0))
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
