using UnityEngine;

namespace TT
{
    // 추적모드
    public class ChaseState : IAIState
    {
        public bool IsInRedBeam;
        private bool isInAgony;

        private Coroutine agonyCoroutine;
        
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
        }

        /// 추적모드중
        public void Execute(AIController ai)
        {
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
            
            // 시야 안에 없어도 어느정도 따라와야 무서움. 때문에 CanSee 제어 말고 플레이어가
            // 안전한 공간 안에 있으면 풀리는 것으로 수정하기
            if (!ai.CanSeePlayer() && !ai.Player.IsDeadCurseState)
            {
                //ai.ChangeState(AIStateType.Wandering);  //⭐⭐⭐깜빡오류해결, 저주컷씬 따로 빼기
            }
            else
            {
                ai.ChasePlayer();
            }
            ai.ChasePlayer();
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
