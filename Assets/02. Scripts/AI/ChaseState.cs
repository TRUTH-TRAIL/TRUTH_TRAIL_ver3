using UnityEngine;
using System;
using System.Threading.Tasks;

namespace TT
{
    // 추적모드
    public class ChaseState : IAIState
    {
        public bool IsInRedBeam;
        private bool isInAgony;

        private Coroutine agonyCoroutine;

        private bool seeCheakCount = false; // 추적->배회 제어

        /// 추적모드 진입
        public void Enter(AIController ai)
        {
            Debug.Log("Entering Chase State");

            IsInRedBeam = false;
            isInAgony = false;
            ai.SetSpeed(ai.runSpeed);
            ai.SetAnimation(ai.runSpeed);

            ai.PlayerSound.StopSound();
            ai.PlayerSound.PlaySound("FindAI", true);
            MainGameSoundManager.Instance.AiSoundPlay("SFX_FindAI");

            Countdown();
        }

        /// 추적모드중
        public void Execute(AIController ai)
        {
            ai.DetectionTimeGuage = 0f; // 추적상태중에 발소리 게이지 중첩 적용 안되도록(사운드 시끄러움)

            // 플레이어 추적 성공
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

            ai.ChasePlayer();
            
            // 추적모드 진입 10초 후부터 플레이어가 시야 안에 없다면 해제
            if (!ai.CanSeePlayer() && seeCheakCount)
            {
                Debug.Log("10초 지나고 시야 안에 없음");
                ai.ChangeState(AIStateType.Wandering);
            }
            else
            {
                ai.ChasePlayer();
            }
            
        }

        public async void Countdown()
        {
            Debug.Log("카운트다운시작");
            seeCheakCount = false;
            await Task.Delay(10000);
            seeCheakCount = true;
        }

        private System.Collections.IEnumerator ExitAgonyAfterDelay(AIController ai, float delay)
        {
            yield return new WaitForSeconds(delay);
            
            ai.SetAnimation("Agony", false);
            ai.ChasePlayer();
            isInAgony = false;

            agonyCoroutine = null;
        }

        /// 추적모드 해제
        public void Exit(AIController ai)
        {
            if (agonyCoroutine is not null)
            {
                ai.StopCoroutine(agonyCoroutine);
                agonyCoroutine = null;
            }
            
            ai.PlayerSound.StopSound();
        }
    }
}
