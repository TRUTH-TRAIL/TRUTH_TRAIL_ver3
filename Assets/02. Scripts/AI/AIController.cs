using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TT
{
    public enum AIStateType
    {
        Wandering,
        Chasing
    }

    public interface IAIState
    {
        void Enter(AIController ai);
        void Execute(AIController ai);
        void Exit(AIController ai);
    }

    public class AIController : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private NavMeshAgent agent;
        public AIStateType currentStateType;
        public Transform PlayerTarget;
        public Transform[] pathPoints; 
        
        [Header("Animation")]
        public Animator animator;
        public float walkSpeed = 3.5f;
        public float runSpeed = 7.0f;
        

        [Header("FOV")]
        public float detectionRange = 10f; 
        public float fieldOfView = 90f;

        [Header("Touch")]
        public float TouchDistance = 2f;
        
        private IAIState currentState;
        private Dictionary<AIStateType, IAIState> states;

        private int currentPathIndex = 0;
        private readonly int Speed = Animator.StringToHash("Speed");

        private void Start()
        {
            states = new Dictionary<AIStateType, IAIState>
            {
                { AIStateType.Wandering, new WanderState() },
                { AIStateType.Chasing, new ChaseState() }
            };

            ChangeState(AIStateType.Wandering);
        }

        public void ChangeState(AIStateType newStateType)
        {
            if (currentState != null)
            {
                currentState.Exit(this);
            }

            currentState = states[newStateType];
            currentStateType = newStateType;
            currentState.Enter(this);
        }

        private void Update()
        {
            currentState.Execute(this);
        }

        public void SetSpeed(float speed)
        {
            agent.speed = speed;
        }

        public void SetAnimation(float value)
        {
            animator.SetFloat(Speed, value);
        }
        
        public void SetAnimation(string value)
        {
            animator.SetTrigger(value);
        }

        public void StopNavMesh()
        {
            agent.ResetPath();    
        }
        
        public void FollowPath()
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                currentPathIndex = (currentPathIndex + 1) % pathPoints.Length;
                agent.destination = pathPoints[currentPathIndex].position;
            }
        }

        public void ChasePlayer()
        {
            agent.destination = GetPlayerPosition();
        }

        public bool CanSeePlayer()
        {
            Vector3 directionToPlayer = PlayerTarget.position - transform.position;
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

            if (angleToPlayer < fieldOfView / 2 && directionToPlayer.magnitude <= detectionRange)
            {
                Ray ray = new Ray(transform.position, directionToPlayer.normalized);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, detectionRange))
                {
                    if (hit.collider.transform == PlayerTarget)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool IsPlayerRunning()
        {
            return Player.Instance.IsRunningState; 
        }

        public bool IsPlayerWalking()
        {
            return Player.Instance.IsWalkingState; 
        }

        public bool IsPlayerMakingNoise()
        {
            return IsPlayerRunning() || IsPlayerWalking(); 
        }

        private Vector3 GetPlayerPosition()
        {
            return PlayerTarget.position; 
        }

        public bool NearestPlayer()
        {
            if (Vector3.Distance(PlayerTarget.position, transform.position) < TouchDistance)
            {
                return true;
            }

            return false;
        }
    }
}
