using UnityEngine;
using UnityEngine.UI;

namespace TT
{
    public class FootGuage : MonoBehaviour
    {
        public AIController AIController;

        public Image FootGuageImage;

        private void Update()
        {
            FootGuageImage.fillAmount = AIController.DetectionTimeGuage / AIController.NeedDetectionTime;
        }
    }
}
