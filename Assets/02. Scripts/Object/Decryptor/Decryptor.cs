using UnityEngine;

namespace TT
{
    public class Decryptor : MonoBehaviour, IDecryptable
    {
        public SpecialPaperController specialPaperController;

        private void Awake()
        {
            if (specialPaperController == null)
            {
                specialPaperController = FindObjectOfType<SpecialPaperController>();
            }
            
            gameObject.layer = LayerMask.NameToLayer("Decryptable");
        }

        public void Decrypt()
        {
            if (specialPaperController != null)
            {
                DecryptManager.Instance.RemoveFalseAndCurseClues(specialPaperController);
            }
            else
            {
                Debug.LogWarning("현재 들고 있는 Special Paper가 없습니다.");
            }
        }
    }
}
