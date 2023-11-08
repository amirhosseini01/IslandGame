using System;
using RPG.Utilities;
using UnityEngine;

namespace RPG.Characters
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
    }
}