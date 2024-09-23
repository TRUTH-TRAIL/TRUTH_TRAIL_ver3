using TMPro;
using UnityEngine;

namespace TT
{
    public class InteractionTextUI : Singleton<InteractionTextUI>
    {
        public GameObject PickRoot;
        public TextMeshProUGUI PickupText;

        protected void Start()
        {
            
            if (PickRoot == null || PickupText == null)
            {
                Debug.LogError("PickRoot or PickupText is not assigned in the inspector.");
            }
        }

        public void SetPickupTextActive(bool isActive, string value)
        {
            PickRoot.SetActive(isActive);
            if (isActive)
            {
                PickupText.text = value;
            }
        }
    }
}