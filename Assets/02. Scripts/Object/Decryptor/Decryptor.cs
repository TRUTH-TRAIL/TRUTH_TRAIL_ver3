using UnityEngine;
using UnityEngine.Events;

namespace TT
{
    public class Decryptor : MonoBehaviour, IDecryptable
    {
        private DecryptManager decryptManager;
        public SpecialPaperHandler specialPaperHandler;

        public UnityEvent OnDecryptEvent;
        
        private void Awake()
        {
            if (specialPaperHandler == null)
            {
                specialPaperHandler = FindObjectOfType<SpecialPaperHandler>();
            }

            decryptManager = FindObjectOfType<DecryptManager>();
            gameObject.layer = LayerMask.NameToLayer("Decryptable");
        }

        public void Decrypt()
        {
            if (specialPaperHandler != null && FindObjectOfType<Player>().isEqiupSpecialPaper)
            {
                OnDecryptEvent?.Invoke();
                decryptManager.RemoveFalseAndCurseClues();
            }
        }
    }
}
