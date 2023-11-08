using System;
using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    public class EnemyController : MonoBehaviour
    {
        public float ChaseRange = 2.5f;
        public float AttackRange = 0.75f;
        
        [NonSerialized]
        public GameObject Player;

        [NonSerialized]
        public Movement MovementCmp;

        [NonSerialized]
        public float DistanceFromPlayer;

        [NonSerialized]
        public Vector3 OriginalPosition;

        private AiBaseState CurrentState;
        [NonSerialized]
        
        public AIReturnState ReturnState = new AIReturnState();
        
        [NonSerialized]
        public AiChaseState ChaseState = new AiChaseState();

        private void Awake()
        {
            CurrentState = ChaseState;
            Player = GameObject.FindWithTag(Constants.PlayerTag);
            MovementCmp = GetComponent<Movement>();

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