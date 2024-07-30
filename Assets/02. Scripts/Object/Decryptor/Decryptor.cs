using UnityEngine;
using UnityEngine.Serialization;

namespace TT
{
    public class Decryptor : MonoBehaviour, IDecryptable
    {
        public SpecialPaperHandler specialPaperHandler;

        private void Awake()
        {
            if (specialPaperHandler == null)
            {
                specialPaperHandler = FindObjectOfType<SpecialPaperHandler>();
            }
            
            gameObject.layer = LayerMask.NameToLayer("Decryptable");
        }

        public void Decrypt()
        {
            if (specialPaperHandler != null)
            {
                DecryptManager.Instance.RemoveFalseAndCurseClues();
            }
            else
            {
                Debug.LogWarning("현재 들고 있는 Special Paper가 없습니다.");
            }
        }
    }
}
