using UnityEngine;

namespace TT
{
    public class WanderState : IAIState
    {
        private float footstepTimer;
        private float footstepInterval = 1.25f;
        
        public void Enter(AIController ai)
        {
            //Debug.Log("Entering Wandering State");
            ai.SetSpeed(ai.walkSpeed);
            ai.SetAnimation(ai.walkSpeed);
        }

        public void Execute(AIController ai)
        {
            ai.FollowPath(); 
            HandleFootsteps(ai);
            
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
        private void HandleFootsteps(AIController ai)
        {
            footstepTimer += Time.deltaTime;
            if (footstepTimer >= footstepInterval)
            {
                footstepTimer = 0f;

                RaycastHit hit;
                Vector3 rayOrigin = ai.transform.position + Vector3.up; 

                if (Physics.Raycast(rayOrigin, Vector3.down, out hit, 5f))
                {
                    GameObject footprint = FootprintPool.Instance.GetFootprint();
                    
                    Vector3 footprintPosition = ai.transform.position;
                    footprintPosition.y = hit.point.y;
                    footprint.transform.position = footprintPosition + Vector3.up * 0.005f;
                    footprint.transform.forward = ai.transform.forward;

                    footprint.transform.rotation = Quaternion.Euler(hit.normal) * Quaternion.AngleAxis(90f, Vector3.forward);

                    footprint.SetActive(true);

                    ai.StartCoroutine(DeactivateFootprintAfterDelay(footprint, 60f));
                }
            }
        }

        private System.Collections.IEnumerator DeactivateFootprintAfterDelay(GameObject footprint, float delay)
        {
            yield return new WaitForSeconds(delay);
            footprint.SetActive(false);
        }
        
        public void Exit(AIController ai)
        {
            //Debug.Log("Exiting Wandering State");
        }
    }
}
