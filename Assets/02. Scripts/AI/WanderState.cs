using UnityEngine;

namespace TT
{
    // 배회모드
    public class WanderState : IAIState
    {
        private float footstepTimer;
        private float footstepInterval = 1.25f;

        /// 배회모드 진입
        public void Enter(AIController ai)
        {
            Debug.Log("Entering Wandering State");

            ai.SetSpeed(ai.walkSpeed);
            ai.SetAnimation(ai.walkSpeed);
        }

        /// 배회모드중
        public void Execute(AIController ai)
        {
            ai.FollowPath(); 
            HandleFootsteps(ai);

            // 추적모드 전환 : 플레이어 발견 or 저주 발동
            if (ai.CanSeePlayer() || ai.Player.IsDeadCurseState)
            {
                ai.ChangeState(AIStateType.Chasing);
                return;
            }

            // 플레이어 공간 감지
            if (ai.IsNearPlayer)
            {
                ai.PlayerSound.PlaySound("NearAI", true);
                // 추적모드 전환 : 플레이어 발소리게이지
                if (ai.Character.speed > 0f && (ai.IsPlayerWalking() || ai.IsPlayerRunning()))
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
                // 플레이어가 멀어진경우 (발소리 게이지 Down)
                ai.DetectionTimeGuage -= Time.deltaTime;
                if (ai.DetectionTimeGuage <= 0f) ai.DetectionTimeGuage = 0f;
                
                ai.PlayerSound.StopSound();
            }
        }

        /// AI 발자국
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

        /// 배회모드 해제
        public void Exit(AIController ai)
        {
            //Debug.Log("Exiting Wandering State");
        }
    }
}
