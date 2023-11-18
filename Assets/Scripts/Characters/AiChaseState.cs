namespace Assets.Scripts.Characters
{
	public class AiChaseState : AiBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.Movement.UpdateAgentSpeed(enemy.Stats.RunSpeed);
        }

        public override void UpdateState(EnemyController enemy)
        {
            if(enemy.DistanceFromPlayer > enemy.ChaseRange)
            {
                enemy.SwitchStates(enemy.ReturnState);
                return;
            }

            if(enemy.DistanceFromPlayer < enemy.AttackRange)
            {
                enemy.SwitchStates(enemy.AttackState);
                return;
            }

            enemy.Movement.MoveAgentByDestination(enemy.Player.transform.position);
        }
    }
}