using UnityEngine;

namespace TT
{
    public class WanderState : IAIState
    {
        private float footstepTimer;
        private float footstepThreshold = 4f; 

        public void Enter(AIController ai)
        {
            Debug.Log("Entering Wandering State");
            ai.SetSpeed(ai.walkSpeed);
            ai.SetAnimation(ai.walkSpeed);
        }

        public void Execute(AIController ai)
        {
            ai.FollowPath(); 

            if (ai.CanSeePlayer())
            {
                ai.ChangeState(AIStateType.Chasing);
            }
            else if (ai.IsPlayerRunning() || footstepTimer >= footstepThreshold || Player.Instance.IsCursed)
            {
                ai.ChangeState(AIStateType.Chasing);
            }

            if (ai.IsPlayerWalking())
            {
                footstepTimer += Time.deltaTime;
            }
            else
            {
                footstepTimer = 0f;
            }
        }

        public void Exit(AIController ai)
        {
            Debug.Log("Exiting Wandering State");
        }
    }
}
