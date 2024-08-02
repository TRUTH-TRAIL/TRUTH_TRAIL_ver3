using UnityEngine;

namespace TT
{
    public class ChaseState : IAIState
    {
        private bool isKilling;
        
        public void Enter(AIController ai)
        {
            Debug.Log("Entering Chasing State");
            isKilling = false;
            ai.SetSpeed(ai.runSpeed);
            ai.SetAnimation(ai.runSpeed);
        }

        public void Execute(AIController ai)
        {
            //PlayerSound.Instance.PlaySound("Chasing", true);
            
            if (ai.NearestPlayer())
            {
                GameManager.Instance.GameOver();
                //ai.ChangeState(AIStateType.Idle);
                return;
            }
            
            if (!ai.CanSeePlayer() && !ai.Player.IsDeadCurseState)
            {
                ai.ChangeState(AIStateType.Wandering);
            }
            else
            {
                ai.ChasePlayer();
            }
        }
        
        public void Exit(AIController ai)
        {
            //PlayerSound.Instance.StopSound();
            Debug.Log("Exiting Chasing State");
        }
    }
}
