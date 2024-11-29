using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TT
{
    public class StartMouse : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Cursor.visible = true; 
            Cursor.lockState = CursorLockMode.None;
        }

    }
}
