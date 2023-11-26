
using UnityEngine;

namespace Assets.Scripts.Characters
{
	public class AiPatrolState : AiBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.Patrol.ResetTimers();
        }

        public override void UpdateState(EnemyController enemy)
        {
            if(enemy.DistanceFromPlayer < enemy.ChaseRange)
            {
                enemy.SwitchStates(enemy.ChaseState);
                return;
            }

            Vector3 oldPosition = enemy.Patrol.GetNextPosition();

            enemy.Patrol.CalculateNextPosition();

            var currentPosition = enemy.transform.position;
            var newPosition = enemy.Patrol.GetNextPosition();
            var offset = newPosition - currentPosition;

            enemy.Movement.MoveAgentByOffset(offset);

            var fartherOutPosition = enemy.Patrol.GetFartherOutPosition();
            var newForwardVector = fartherOutPosition - currentPosition;
            newForwardVector.y = 0;

            enemy.Movement.Rotate(newForwardVector);

            if(oldPosition == newPosition)
            {
                enemy.Movement.IsMoving = false;
            }
        }
    }

}