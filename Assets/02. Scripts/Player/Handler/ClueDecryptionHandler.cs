using UnityEngine;

namespace TT
{
    public class ClueDecryptionHandler : MonoBehaviour
    {
        public float DecryptionRange = 3.0f;
        public LayerMask DecryptionMask;
        public string DecryptionString = "해독하기";
        private Camera cam;

        private void Awake()
        {
            cam = Camera.main;
        }

        private void Update()
        {
            CheckForDecrypt();
        }

        private void CheckForDecrypt()
        {
            Collider collider = RaycastUtil.TryGetPickupableCollider(cam, DecryptionRange, DecryptionMask);
            if (collider != null)
            {
                IDecryptable decryptable = collider.GetComponent<IDecryptable>();
                if (decryptable != null)
                {
                    HandleDecrypt(decryptable);
                }
                else
                {
                    InteractionTextUI.Instance.SetPickupTextActive(false, DecryptionString);
                }
            }
            else
            {
                InteractionTextUI.Instance.SetPickupTextActive(false, DecryptionString);
            }
        }

        private void HandleDecrypt(IDecryptable decryptable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                decryptable.Decrypt();
            }
            else
            {
                InteractionTextUI.Instance.SetPickupTextActive(true, DecryptionString);
            }
        }
    }
}