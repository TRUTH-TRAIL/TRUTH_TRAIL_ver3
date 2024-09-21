using UnityEngine;

namespace TT
{
    public class ChaseState : IAIState
    {
        private bool isKilling;
        public bool IsInRedBeam;
        
        private float footstepTimer;
        private float footstepInterval = 0.85f;

        private bool isInAgony;

        public void Enter(AIController ai)
        {
            //Debug.Log("Entering Chasing State");
            isKilling = false;
            IsInRedBeam = false;
            isInAgony = false;
            ai.SetSpeed(ai.runSpeed);
            ai.SetAnimation(ai.runSpeed);
        }


        public void Execute(AIController ai)
        {
            //PlayerSound.Instance.PlaySound("Chasing", true);
            ai.PlayerSound.PlaySound("CruelDollDetection", true);
            //HandleFootsteps(ai);
            
            if (ai.NearestPlayer())
            {
                GameManager.Instance.GameOver();
                //ai.ChangeState(AIStateType.Idle);
                return;
            }
            
            //손전등에 당하면 고통스러워하며 애니메이션 실행되게
            if (ai.IsInRedBeam && !isInAgony)
            {
                ai.StopNavMesh();
                ai.SetAnimation("Agony", true);
                IsInRedBeam = true;
                isInAgony = true;
                
                return;
            }
            else if (!ai.IsInRedBeam && isInAgony && IsInRedBeam)
            {
                IsInRedBeam = false;
                ai.StartCoroutine(ExitAgonyAfterDelay(ai, 3f));
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
        
        private void HandleFootsteps(AIController ai)
        {
            footstepTimer += Time.deltaTime;
            if (footstepTimer >= footstepInterval)
            {
                footstepTimer = 0f;

                RaycastHit hit;
                Vector3 rayOrigin = ai.transform.position; 

                if (Physics.Raycast(rayOrigin, Vector3.down, out hit, 5f, 1<<16))
                {
                    GameObject footprint = FootprintPool.Instance.GetFootprint();
                    
                    footprint.transform.position = hit.point + Vector3.up * 0.15f;
                    footprint.transform.forward = ai.transform.forward;

                    footprint.transform.rotation = Quaternion.Euler(hit.normal) * Quaternion.AngleAxis(90f, Vector3.forward);

                    footprint.SetActive(true);

                    ai.StartCoroutine(DeactivateFootprintAfterDelay(footprint, 60f));
                }
            }
        }
        
        private System.Collections.IEnumerator ExitAgonyAfterDelay(AIController ai, float delay)
        {
            yield return new WaitForSeconds(delay);
            
            ai.SetAnimation("Agony", false);
            ai.ChasePlayer();
            isInAgony = false;
        }
        
        private System.Collections.IEnumerator DeactivateFootprintAfterDelay(GameObject footprint, float delay)
        {
            yield return new WaitForSeconds(delay);
            footprint.SetActive(false);
        }
        
        public void Exit(AIController ai)
        {
            //PlayerSound.Instance.StopSound();
            Debug.Log("Exiting Chasing State");
        }
    }
}
