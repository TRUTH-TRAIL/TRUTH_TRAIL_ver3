using System.Collections.Generic;
using ECM2;
using Micosmo.SensorToolkit;
using UnityEngine;
using UnityEngine.AI;

namespace TT
{
    public enum AIStateType
    {
        Wandering,
        Chasing,
        Idle
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
        public Transform PathsContainer;
        private Transform[] paths;
        public Character Character;
        public Sensor DetectionSensor;
        public Sensor DetectionRangeSensor;
        public bool IsNearPlayer => DetectionRangeSensor.GetNearestDetection() != null;
        public PlayerSound PlayerSound;
        public Player Player;
        
        [Header("Animation")]
        public Animator animator;
        public float walkSpeed = 3.5f;
        public float runSpeed = 7.0f;

        [Header("Touch")]
        public float TouchDistance = 2f;
        
        private IAIState currentState;
        private Dictionary<AIStateType, IAIState> states;

        private int currentPathIndex = 0;
        private readonly int Speed = Animator.StringToHash("Speed");

        [Header("Detection")]
        public float DetectionTimeGuage;
        public float NeedDetectionTime;
        
        
        private void Start()
        {
            Character = FindObjectOfType<Character>();
            PlayerTarget = Character.transform;
            PlayerSound = FindObjectOfType<PlayerSound>();
            Player = FindObjectOfType<Player>();
            
            states = new Dictionary<AIStateType, IAIState>
            {
                { AIStateType.Wandering, new WanderState() },
                { AIStateType.Chasing, new ChaseState() },
                { AIStateType.Idle, new IdleState() }
            };

            ChangeState(AIStateType.Wandering);

            paths = PathsContainer.GetComponentsInChildren<Transform>();
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
                currentPathIndex = Random.Range(0, paths.Length);
                agent.destination = paths[currentPathIndex].position;
            }
        }

        public void ChasePlayer()
        {
            agent.destination = GetPlayerPosition();
        }

        public bool CanSeePlayer()
        {
            return DetectionSensor.GetNearestDetection()!= null;
        }

        public bool IsPlayerRunning()
        {
            return Player.IsRunningState; 
        }

        public bool IsPlayerWalking()
        {
            return Player.IsWalkingState; 
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
