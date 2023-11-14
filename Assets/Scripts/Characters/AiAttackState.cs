namespace Assets.Scripts.Characters
{
	public class AiAttackState : AiBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.Movement.StopMovingAgent();
        }
        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.DistanceFromPlayer > enemy.AttackRange)
            {
                enemy.SwitchStates(enemy.ChaseState);
                return;
            }
        }
    }
}
