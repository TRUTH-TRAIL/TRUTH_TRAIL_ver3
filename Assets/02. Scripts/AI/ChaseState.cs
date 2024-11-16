using UnityEngine;

namespace TT
{
    public class ChaseState : IAIState
    {
        public bool IsInRedBeam;
        private bool isInAgony;

        private Coroutine agonyCoroutine;
        
        public void Enter(AIController ai)
        {
            Debug.Log("Entering Chase State");

            IsInRedBeam = false;
            isInAgony = false;
            ai.SetSpeed(ai.runSpeed);
            ai.SetAnimation(ai.runSpeed);

            ai.PlayerSound.PlaySound("FindAI", true);
        }


        public void Execute(AIController ai)
        {
            if (ai.NearestPlayer())
            {
                GameManager.Instance.GameOver();
                //ai.ChangeState(AIStateType.Idle);
                return;
            }
            
            if (ai.IsInRedBeam && !isInAgony)
            {
                ai.StopNavMesh();
                ai.SetAnimation("Agony", true);
                IsInRedBeam = true;
                isInAgony = true;
                
                return;
            }
            
            if (!ai.IsInRedBeam && isInAgony && IsInRedBeam)
            {
                IsInRedBeam = false;
                agonyCoroutine ??= ai.StartCoroutine(ExitAgonyAfterDelay(ai, 3f));    
            }

            if (IsInRedBeam || isInAgony)
            {
                return;
            }

            if (ai.State is GameState.Exorcism)
            {
                ai.ChasePlayer();
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
        
        private System.Collections.IEnumerator ExitAgonyAfterDelay(AIController ai, float delay)
        {
            yield return new WaitForSeconds(delay);
            
            ai.SetAnimation("Agony", false);
            ai.ChasePlayer();
            isInAgony = false;

            agonyCoroutine = null;
        }
        
        public void Exit(AIController ai)
        {
            if (agonyCoroutine is not null)
            {
                ai.StopCoroutine(agonyCoroutine);
                agonyCoroutine = null;
            }
            
            ai.PlayerSound.StopSound();
            //Debug.Log("Exiting Chasing State");
        }
    }
}
