using UnityEngine;

namespace Assets.Scripts.Characters
{
	public class AiChaseState : AiBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            Debug.Log("--> I'm here: AiChaseState:EnterState");
        }

        public override void UpdateState(EnemyController enemy)
        {
            if(enemy.DistanceFromPlayer > enemy.ChaseRange)
            {
                enemy.SwitchStates(enemy.ReturnState);
                return;
            }

            Debug.Log($"{enemy.AttackRange}");
            if(enemy.DistanceFromPlayer < enemy.AttackRange)
            {
                enemy.SwitchStates(enemy.AttackState);
                return;
            }

            enemy.Movement.MoveAgentByDestination(enemy.Player.transform.position);
        }
    }
}