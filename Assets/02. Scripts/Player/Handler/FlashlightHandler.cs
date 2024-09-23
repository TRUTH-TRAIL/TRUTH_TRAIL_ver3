using UnityEngine;

namespace TT
{
    public class FlashlightHandler : MonoBehaviour
    {
        public static FlashlightHandler Instance;

        [Header("Flashlights")] 
        public GameObject WhiteFlashlight;
        public GameObject BlueFlashlight;
        public GameObject RedFlashlight;

        [Header("KeyCodes")] 
        public KeyCode ToggleWhiteFlashlightKey = KeyCode.Alpha1;
        public KeyCode ToggleBlueFlashlightKey = KeyCode.Alpha2;
        public KeyCode ToggleRedFlashlightKey = KeyCode.Alpha3;

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

            currentFlashlight = WhiteFlashlight;
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

            if (Input.GetKeyDown(ToggleRedFlashlightKey) && Player.Instance.isAcquiredBattery)
            {
                ToggleFlashlight(RedFlashlight);
            }
            
            if (Input.GetKeyDown(ToggleBlueFlashlightKey))
            {
                ToggleFlashlight(BlueFlashlight);
            }
        }

        private void ToggleFlashlight(GameObject flashlight)
        {
            if (flashlight == null) return;
            
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
    }
}
