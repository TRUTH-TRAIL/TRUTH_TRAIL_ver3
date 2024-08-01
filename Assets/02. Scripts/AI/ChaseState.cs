using UnityEngine;

namespace TT
{
    public class ChaseState : IAIState
    {
        private AIController aiController;

        private bool isKilling;
        
        public void Enter(AIController ai)
        {
            Debug.Log("Entering Chasing State");
            aiController = ai;
            ai.SetSpeed(ai.runSpeed);
            ai.SetAnimation(ai.runSpeed);
        }

        public void Execute(AIController ai)
        {
            if (isKilling) return;
            
            ai.ChasePlayer();

            if (ai.NearestPlayer())
            {
                Kill();
            }
            
            if (!ai.CanSeePlayer() && !ai.IsPlayerMakingNoise())
            {
                ai.ChangeState(AIStateType.Wandering);
            }
        }
        
        public void Exit(AIController ai)
        {
            Debug.Log("Exiting Chasing State");
        }

        private void Kill()
        {
            Debug.Log("Killing Chasing State");
            isKilling = true;
            aiController.StopNavMesh();
            aiController.SetSpeed(0);
            aiController.SetAnimation("Kill");
        }
    }
}
