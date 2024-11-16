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
                MainGameSoundManager.Instance.PlaySFX("Click_1");   // 해독 사운드 교체하기
            }
            else
            {
                InteractionTextUI.Instance.SetPickupTextActive(true, BaseString);
            }
        }
    }
}