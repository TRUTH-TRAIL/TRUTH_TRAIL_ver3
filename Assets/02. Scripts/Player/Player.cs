using UnityEngine;

namespace TT
{
    public class Player : Singleton<Player>
    {
        public bool IsCursed { get; private set; }
        public bool isAcquiredSpecialPaper;
        public bool isEqiupSpecialPaper;

        private ICurse currentCurse;
        public ICurse CurrentCurse
        {
            get => currentCurse;
            set
            {
                currentCurse = value;
                IsCursed = currentCurse != null;
            }
        }

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
