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
            
            if (ai.CanSeePlayer() || ai.Player.IsDeadCurseState)
            {
                ai.ChangeState(AIStateType.Chasing);
                return;
            }
            
            if (ai.IsNearPlayer)
            {
                ai.PlayerSound.PlaySound("CruelDollDetection", true);
     
                if (ai.Character.speed > 0f && ai.IsPlayerWalking())
                {
                    ai.DetectionTimeGuage += Time.deltaTime;

                    if (ai.DetectionTimeGuage >= ai.NeedDetectionTime)
                    {
                        ai.DetectionTimeGuage = ai.NeedDetectionTime;

                        ai.ChangeState(AIStateType.Chasing);
                    }
                }
            }
            else
            {
                ai.DetectionTimeGuage -= Time.deltaTime;
                if (ai.DetectionTimeGuage <= 0f) ai.DetectionTimeGuage = 0f;
                
                ai.PlayerSound.StopSound();
            }
        }

        public void Exit(AIController ai)
        {
            Debug.Log("Exiting Wandering State");
        }
    }
}
