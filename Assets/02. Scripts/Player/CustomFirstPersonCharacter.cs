using ECM2.Examples.FirstPerson;
using UnityEngine;

namespace TT
{
    public class CustomFirstPersonCharacter : FirstPersonCharacter
    {
        [Header("Run")]
        [SerializeField] private float speedMutiplier = 1.3f;
        [SerializeField] private KeyCode runKey = KeyCode.LeftShift;
        
        private float originSpeed;
        private Player Player;
        
        protected override void Start()
        {
            Player = FindObjectOfType<Player>();
            
            originSpeed = maxWalkSpeed;
        }

        private void Update()
        {
            if (Input.GetKeyDown(runKey))
            {
                Player.IsRunningState = true;
                maxWalkSpeed *= speedMutiplier;          
            }
            else if (Input.GetKeyUp(runKey))
            {
                Player.IsRunningState = false;
                maxWalkSpeed = originSpeed;     
            }
            
            Player.IsSlowWalkingState = IsCrouched() && !IsWalking() && !IsRunning();
            Player.IsWalkingState = IsWalking() && !IsCrouched() && !IsRunning();

            FootStep();
        }

        private bool IsRunning()
        {
            return Player.IsRunningState;
        }

        // SoundManager
        private void FootStep()
        {
            if (Player.IsWalkingState)
                MainGameSoundManager.Instance.PlayerState("walking");
            else if (Player.IsSlowWalkingState)
                MainGameSoundManager.Instance.PlayerState("slowWalking");
            else if(Player.IsRunningState)
                MainGameSoundManager.Instance.PlayerState("running");
        }
    }
}
