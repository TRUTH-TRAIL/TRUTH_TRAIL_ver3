using System.Collections.Generic;
using ECM2;
using Micosmo.SensorToolkit;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

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
        public GameState State;
        public AIStateType CurrentStateType;
        public Transform PathsContainer;
        public float walkSpeed = 3.5f;
        public float runSpeed = 7.0f;
        
        private LOSSensor DetectionSensor;
        private RangeSensor DetectionRangeSensor;
        
        public Transform PlayerTarget { get; private set; }
        public PlayerSound PlayerSound { get; private set; }
        public Player Player { get; private set; }
        public Character Character { get; private set; }
        public bool IsNearPlayer => DetectionRangeSensor.GetNearestDetection() != null;

        
        [Header("Touch & Detection")]
        public float TouchDistance = 2f;
        public float DetectionTimeGuage;
        public float NeedDetectionTime;
        
        private Transform[] paths;
        private NavMeshAgent agent;
        private Animator animator;
        private IAIState currentState;      // 현재 AI 상태
        private Dictionary<AIStateType, IAIState> states;
        private int currentPathIndex = 0;
       
        private readonly int Speed = Animator.StringToHash("Speed");
        
        public bool IsInRedBeam { get; set; }

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponentInChildren<Animator>();
            DetectionSensor = GetComponentInChildren<LOSSensor>();
            DetectionRangeSensor = GetComponentInChildren<RangeSensor>();
        }

        private void Start()
        {
            InitPlayerComponent();
            
            if (State is GameState.MainGame)
            {
                states = new Dictionary<AIStateType, IAIState>
                {
                    { AIStateType.Wandering, new WanderState() },
                    { AIStateType.Chasing, new ChaseState() },
                    { AIStateType.Idle, new IdleState() }
                };
                paths = PathsContainer.GetComponentsInChildren<Transform>();
                ChangeState(AIStateType.Wandering);    
            }
            else if (State is GameState.Exorcism)
            {
                states = new Dictionary<AIStateType, IAIState>
                {
                    { AIStateType.Chasing, new ChaseState() },
                    { AIStateType.Idle, new IdleState() }
                };
                ChangeState(AIStateType.Chasing);
            }
        }

        private void InitPlayerComponent()
        {
            Character = FindObjectOfType<Character>();
            PlayerTarget = Character.transform;
            PlayerSound = FindObjectOfType<PlayerSound>();
            Player = FindObjectOfType<Player>();
        }

        // AI 상태 변경
        public void ChangeState(AIStateType newStateType)
        {
            if (currentState != null)
            {
                currentState.Exit(this);
            }

            currentState = states[newStateType];
            CurrentStateType = newStateType;
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

        public void SetAnimation(string value, bool on)
        {
            animator.SetBool(value, on);
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

        // destination set
        public void ChasePlayer()
        {
            agent.destination = GetPlayerPosition();
        }

        // 플레이어 감지 상태 get
        public bool CanSeePlayer()
        {
            return DetectionSensor.GetNearestDetection()!= null;
        }

        // 플레이어 run 상태 get
        public bool IsPlayerRunning()
        {
            return Player.IsRunningState; 
        }

        // 플레이어 walk 상태 get
        public bool IsPlayerWalking()
        {
            return Player.IsWalkingState; 
        }

        private Vector3 GetPlayerPosition()
        {
            return PlayerTarget.position; 
        }

        // 플레이어 추적 성공 거리 체크, return
        public bool NearestPlayer()
        {
            if(FloorComparison(PlayerTarget.position.y, transform.position.y))
            {
                Vector3 playerPos = new Vector3(PlayerTarget.position.x, 0, PlayerTarget.position.z);
                Vector3 objectPos = new Vector3(transform.position.x, 0, transform.position.z);

                if (Vector3.Distance(playerPos, objectPos) < TouchDistance)
                {
                    return true;
                }

                return false;
            }
            else
                return false;
        }

        // 1, 2층 비교
        public bool FloorComparison(float playerY, float aiY)
        {
            int playerFloor, aiFloor;

            if (playerY >= 0f)
                playerFloor = 2;
            else
                playerFloor = 1;

            if (aiY >= 2.5f)
                aiFloor = 2;
            else
                aiFloor = 1;

            if (playerFloor == aiFloor)
                return true;
            else
            {
                Debug.Log("같은층 아님");
                return false;
            }
                
        }
    }
}
