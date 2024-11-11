using UnityEngine;

namespace TT
{
    public class Player : MonoBehaviour
    {
        public static Player Instance;

        private void Awake()
        {
            Instance = this; //lazy
        }

        public bool IsCursed { get; private set; }
        public bool isAcquiredSpecialPaper;
        public bool isAcquiredBattery;
        
        public bool isEqiupSpecialPaper;
        public bool isEqiupKey;
        public bool isEqiupLighter;
        public bool isEqiupSpecialCandle1;
        public bool isEqiupSpecialCandle2;
        public bool isEqiupSpecialCandle3;
        public bool isEqiupCross;
    
        public bool IsWalkingState;
        public bool IsRunningState;
        public bool IsSlowWalkingState;

        public bool IsDeadCurseState;
        public bool IsDead;
        
        
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
            
            IsDeadCurseState = false;
        }

        public void Dead()
        {
            IsDead = true;
            //죽는 애니메이션 실행
        }
    }
}
