
using UnityEngine;

namespace Assets.Scripts.Characters
{
	public class AIReturnState : AiBaseState
    {
        private Vector3 _targetPosition;
        public override void EnterState(EnemyController enemy)
        {
            enemy.Movement.UpdateAgentSpeed(enemy.Stats.WalkSpeed);

            if(enemy.Patrol is null)
            {
                enemy.Movement.MoveAgentByDestination(
                    enemy._originalPosition
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

            if(enemy.Movement.ReachedDestination() && enemy.Patrol is not null)
            {
                enemy.SwitchStates(enemy.PatrolState);
                return;
            }
        }
    }
}