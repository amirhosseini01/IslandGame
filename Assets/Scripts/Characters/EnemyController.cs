using System;
using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    public class EnemyController : MonoBehaviour
    {
        public float ChaseRange = 3.5f;
        public float AttackRange = 1.16f;
        
        [NonSerialized]
        public GameObject Player;

        [NonSerialized]
        public Movement Movement;
        
        [NonSerialized]
        public Patrol Patrol;

        [NonSerialized]
        public float DistanceFromPlayer;

        [NonSerialized]
        public Vector3 OriginalPosition;

        [NonSerialized]
        public AIReturnState ReturnState = new AIReturnState();
        
        [NonSerialized]
        public AiChaseState ChaseState = new AiChaseState();

        [NonSerialized]
        public AiAttackState AttackState = new AiAttackState();

        [NonSerialized]
        public AiPatrolState PatrolState = new AiPatrolState();
        
        [NonSerialized]
        public AiDefeatedState DefeatedState = new AiDefeatedState();

        public CharacterStatsScriptableObj Stats;
        
        [NonSerialized]
        public Combat CombatComponent;

        private AiBaseState _currentState;
        private Health _healthComponent;

        public void SwitchStates(AiBaseState newState)
        {
            _currentState = newState;
            _currentState.EnterState(this);
        }

        private void Awake()
        {
            _currentState = ChaseState;
            Player = GameObject.FindWithTag(Constants.PlayerTag);
            Movement = GetComponent<Movement>();
            Patrol = GetComponent<Patrol>();
            CombatComponent = GetComponent<Combat>();
            _healthComponent = GetComponent<Health>();

            OriginalPosition = transform.position;
        }

        private void OnEnable()
        {
            _healthComponent.OnStartDefeated += HandleStartDefeated;
        }

        private void OnDisable()
        {
            _healthComponent.OnStartDefeated -= HandleStartDefeated;
        }

        private void Start()
        {
            _currentState.EnterState(this);

            _healthComponent.HealthPoints = Stats.Health;
            CombatComponent.Damage = Stats.Damage;
        }

        private void Update()
        {
            CalculateDistanceFromPlayer();

            _currentState.UpdateState(this);
        }

        private void CalculateDistanceFromPlayer()
        {
            if (Player == null)
			{
				return;
			}

			var enemyPosition = transform.position;
            var playerPosition = Player.transform.position;

            DistanceFromPlayer = Vector3.Distance(enemyPosition, playerPosition);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(
                transform.position,
                ChaseRange
            );
        }

        private void HandleStartDefeated()
        {
            SwitchStates(DefeatedState);
            _currentState.EnterState(this);
        }
    }
}