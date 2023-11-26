
using UnityEngine;

namespace Assets.Scripts.Characters
{
    public class AIReturnState : AiBaseState
    {
        private Vector3 _targetPosition;
        public override void EnterState(EnemyController enemy)
        {
            enemy.Movement.UpdateAgentSpeed(enemy.Stats.WalkSpeed);

            if (enemy.Patrol is null)
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
            if (enemy.DistanceFromPlayer < enemy.ChaseRange)
            {
                enemy.SwitchStates(enemy.ChaseState);
                return;
            }

            if (enemy.Movement.ReachedDestination())
            {
                if (enemy.Patrol is not null)
                {
                    enemy.SwitchStates(enemy.PatrolState);
                    return;
                }
                else
                {
                    enemy.Movement.IsMoving = false;
                    enemy.Movement.Rotate(enemy.Movement.OriginalForwardVector);
                }
            }
            else
            {
                Vector3 newForwardVector;
                if (enemy.Patrol is null)
                {
                    newForwardVector = enemy.OriginalPosition -
                        enemy.transform.position;
                        newForwardVector.y = 0;
                    
                    enemy.Movement.Rotate(newForwardVector);
                }
                else
                {
                    newForwardVector = _targetPosition - enemy.transform.position;
                    newForwardVector.y = 0;
                    
                    enemy.Movement.Rotate(newForwardVector);
                }
            }
        }
    }
}