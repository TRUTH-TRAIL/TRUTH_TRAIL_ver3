using UnityEngine;

namespace TT
{
    public class IdleState : IAIState
    {
        public void Enter(AIController ai)
        {
            Debug.Log("Entering Idle State");
            ai.StopNavMesh();
            ai.SetSpeed(0);
            ai.SetAnimation("Scream");
        }

        public void Execute(AIController ai)
        {
            //PlayerSound.Instance.PlaySound("Screaming", true);
        }

        public void Exit(AIController ai)
        {
            Debug.Log("Exit Idle State");
        }
    }
}
