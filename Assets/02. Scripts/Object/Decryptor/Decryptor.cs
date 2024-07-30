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
            if (specialPaperHandler != null && Player.Instance.isEqiupSpecialPaper)
            {
                DecryptManager.Instance.RemoveFalseAndCurseClues();
            }
        }
    }
}
