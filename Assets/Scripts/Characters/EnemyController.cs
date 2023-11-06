using RPG.Utilities;
using UnityEngine;

namespace RPG.Characters
{
    public class EnemyController : MonoBehaviour
    {
        public GameObject Player;
        public float ChaseRange = 2.5f;
        public float AttackRange = 0.75f;
        public float DistanceFromPlayer;

        private void Awake()
        {
            Player = GameObject.FindWithTag(Constants.PlayerTag);
        }

        private void Update()
        {
            CalculateDistanceFromPlayer();
        }

        private void CalculateDistanceFromPlayer()
        {
            if(Player == null) return;

            Vector3 enemyPosition = transform.position;
            Vector3 playerPosition = Player.transform.position;

            DistanceFromPlayer = Vector3.Distance(enemyPosition, playerPosition);
        }
    }
}