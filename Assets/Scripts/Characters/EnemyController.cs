using System;
using RPG.Utilities;
using UnityEngine;

namespace RPG.Characters
{
    public class EnemyController : MonoBehaviour
    {
        private GameObject Player;
        public float ChaseRange = 2.5f;
        public float AttackRange = 0.75f;

        [NonSerialized]
        public Movement MovementCmp;

        [NonSerialized]
        public float DistanceFromPlayer;

        [NonSerialized]
        public Vector3 OriginalPosition;

        private AIBaseState CurrentState;
        private AIReturnState ReturnState = new AIReturnState();

        private void Awake()
        {
            CurrentState = ReturnState;
            Player = GameObject.FindWithTag(Constants.PlayerTag);
            MovementCmp = GetComponent<Movement>();

            OriginalPosition = transform.position;
        }

        private void Start()
        {
            CurrentState.EnterState(this);
        }

        private void Update()
        {
            CalculateDistanceFromPlayer();
            ChasePlayer();

            CurrentState.UpdateState(this);
        }

        private void CalculateDistanceFromPlayer()
        {
            if (Player == null) return;

            Vector3 enemyPosition = transform.position;
            Vector3 playerPosition = Player.transform.position;

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

        private void ChasePlayer()
        {
            if (DistanceFromPlayer > ChaseRange) return;

            MovementCmp.MoveAgentByDestination(Player.transform.position);
        }
    }
}