using UnityEngine;

namespace TT
{
    public class FlashlightHandler : MonoBehaviour
    {
        public static FlashlightHandler Instance;

        [Header("Flashlights")] 
        public GameObject WhiteFlashlight;
        public GameObject RedFlashlight;

        [Header("KeyCodes")] 
        public KeyCode ToggleWhiteFlashlightKey = KeyCode.Alpha1;
        public KeyCode ToggleRedFlashlightKey = KeyCode.Alpha2;

        private GameObject currentFlashlight;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            if (WhiteFlashlight == null && RedFlashlight == null) return;
            
            HandleInput();
        }

        private void HandleInput()
        {
            if (Input.GetKeyDown(ToggleWhiteFlashlightKey))
            {
                ToggleFlashlight(WhiteFlashlight);
            }

            if (Input.GetKeyDown(ToggleRedFlashlightKey))
            {
                ToggleFlashlight(RedFlashlight);
            }

            if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                SwitchFlashlight();
            }
        }

        private void ToggleFlashlight(GameObject flashlight)
        {
            if (currentFlashlight == flashlight)
            {
                currentFlashlight.SetActive(!currentFlashlight.activeSelf);
            }
            else
            {
                if (currentFlashlight != null)
                {
                    currentFlashlight.SetActive(false);
                }

                currentFlashlight = flashlight;
                currentFlashlight.SetActive(true);
            }
        }

        private void SwitchFlashlight()
        {
            if (currentFlashlight == WhiteFlashlight)
            {
                ToggleFlashlight(RedFlashlight);
            }
            else if (currentFlashlight == RedFlashlight)
            {
                ToggleFlashlight(WhiteFlashlight);
            }
        }
    }
}
