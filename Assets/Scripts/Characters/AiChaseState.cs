using System.Diagnostics;

namespace Assets.Scripts.Characters
{
	public class AiChaseState : AiBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            Debug.WriteLine("--> I'm here: AiChaseState:EnterState");
        }

        public override void UpdateState(EnemyController enemy)
        {
            if(enemy.DistanceFromPlayer > enemy.ChaseRange)
            {
                enemy.SwitchStates(enemy.ReturnState);
                return;
            }

            enemy.MovementCmp.MoveAgentByDestination(enemy.Player.transform.position);
        }
    }
}