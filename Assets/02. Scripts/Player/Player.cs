using UnityEngine;

namespace TT
{
    public class Player : Singleton<Player>
    {
        public bool IsCursed;
        public bool isAcquiredSpecialPaper;
        public bool isEqiupSpecialPaper;

        public ICurse CurrentCurse;

        public void RemoveComponent()
        {
            if (CurrentCurse is MonoBehaviour curseMonoBehaviour)
            {
                IsCursed = false;
                Destroy(curseMonoBehaviour);
                CurrentCurse = null;
            }
        }
    }
}
