using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
    public class AiChaseState : AiBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            throw new System.NotImplementedException();
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