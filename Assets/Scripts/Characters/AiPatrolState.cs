namespace Assets.Scripts.Characters
{
	public class AiPatrolState : AiBaseState
    {
        public override void EnterState(EnemyController enemy)
        {

        }

        public override void UpdateState(EnemyController enemy)
        {
            if(enemy.DistanceFromPlayer < enemy.ChaseRange)
            {
                enemy.SwitchStates(enemy.ChaseState);
                return;
            }

            enemy.Patrol.CalculateNextPosition();

            var currentPosition = enemy.transform.position;
            var newPosition = enemy.Patrol.GetNextPosition();
            var offset = newPosition - currentPosition;

            enemy.Movement.MoveAgentByOffset(offset);
        }
    }

}