using UnityEngine;

namespace TT
{
    public class ClueDecryptionController : BaseController<IDecryptable>
    {
        protected override void HandleAction(IDecryptable decryptable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                decryptable.Decrypt();
            }
            else
            {
                InteractionTextUI.Instance.SetPickupTextActive(true, BaseString);
            }
        }
    }
}