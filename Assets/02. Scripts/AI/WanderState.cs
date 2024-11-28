using UnityEngine;

namespace TT
{
    // ��ȸ���
    public class WanderState : IAIState
    {
        private float footstepTimer;
        private float footstepInterval = 1.25f;

        /// ��ȸ��� ����
        public void Enter(AIController ai)
        {
            Debug.Log("Entering Wandering State");

            ai.SetSpeed(ai.walkSpeed);
            ai.SetAnimation(ai.walkSpeed);
        }

        /// ��ȸ�����
        public void Execute(AIController ai)
        {
            ai.FollowPath(); 
            HandleFootsteps(ai);

            // ������� ��ȯ : �÷��̾� �߰� or ���� �ߵ�
            if (ai.CanSeePlayer() || ai.Player.IsDeadCurseState)
            {
                ai.ChangeState(AIStateType.Chasing);
                return;
            }

            // �÷��̾� ���� ����
            if (ai.IsNearPlayer)
            {
                ai.PlayerSound.PlaySound("NearAI", true);
                // ������� ��ȯ : �÷��̾� �߼Ҹ�������
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
                // �÷��̾ �־������ (�߼Ҹ� ������ Down)
                ai.DetectionTimeGuage -= Time.deltaTime;
                if (ai.DetectionTimeGuage <= 0f) ai.DetectionTimeGuage = 0f;
                
                ai.PlayerSound.StopSound();
            }
        }

        /// AI ���ڱ�
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

        /// ��ȸ��� ����
        public void Exit(AIController ai)
        {
            //Debug.Log("Exiting Wandering State");
        }
    }
}
