
using UnityEngine;

namespace Assets.Scripts.Characters
{
	public class AIReturnState : AiBaseState
    {
        private Vector3 _targetPosition;
        public override void EnterState(EnemyController enemy)
        {
            if(enemy.Patrol is null)
            {
                enemy.Movement.MoveAgentByDestination(
                    enemy.OriginalPosition
                );
            }
            else
            {
                _targetPosition = enemy.Patrol.GetNextPosition();

                enemy.Movement.MoveAgentByDestination(_targetPosition);
            }
        }

        public override void UpdateState(EnemyController enemy)
        {
            if(enemy.DistanceFromPlayer < enemy.ChaseRange)
            {
                enemy.SwitchStates(enemy.ChaseState);
                return;
            }
        }
    }
}