using System;
using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    public class EnemyController : MonoBehaviour
    {
        public float ChaseRange = 2.5f;
        public float AttackRange = 1.16f;
        
        [NonSerialized]
        public GameObject Player;

        [NonSerialized]
        public Movement Movement;
        
        [NonSerialized]
        public Patrol Patrol;

        [NonSerialized]
        public float DistanceFromPlayer;

        private AiBaseState CurrentState;
        
        [NonSerialized]
        public Vector3 OriginalPosition;

        [NonSerialized]
        public AIReturnState ReturnState = new AIReturnState();
        
        [NonSerialized]
        public AiChaseState ChaseState = new AiChaseState();

        [NonSerialized]
        public AiAttackState AttackState = new AiAttackState();

        private void Awake()
        {
            CurrentState = ChaseState;
            Player = GameObject.FindWithTag(Constants.PlayerTag);
            Movement = GetComponent<Movement>();
            Patrol = GetComponent<Patrol>();

            OriginalPosition = transform.position;
        }

        public void SwitchStates(AiBaseState newState)
        {
            CurrentState = newState;
            CurrentState.EnterState(this);
        }

        private void Start()
        {
            CurrentState.EnterState(this);
        }

        private void Update()
        {
            CalculateDistanceFromPlayer();

            CurrentState.UpdateState(this);
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
    }
}