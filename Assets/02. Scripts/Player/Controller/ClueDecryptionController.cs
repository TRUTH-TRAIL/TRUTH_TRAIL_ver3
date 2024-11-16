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
                MainGameSoundManager.Instance.PlaySFX("Click_1");   // �ص� ���� ��ü�ϱ�
            }
            else
            {
                InteractionTextUI.Instance.SetPickupTextActive(true, BaseString);
            }
        }
    }
}