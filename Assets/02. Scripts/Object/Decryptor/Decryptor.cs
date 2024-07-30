using UnityEngine;
using UnityEngine.Serialization;

namespace TT
{
    public class Decryptor : MonoBehaviour, IDecryptable
    {
        private DecryptManager decryptManager;
        public SpecialPaperHandler specialPaperHandler;
        
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
            if (specialPaperHandler != null && Player.Instance.isEqiupSpecialPaper)
            {
                decryptManager.RemoveFalseAndCurseClues();
            }
        }
    }
}
