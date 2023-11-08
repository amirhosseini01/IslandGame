using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
    public class AIReturnState : AiBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.MovementCmp.MoveAgentByDestination(
                enemy.OriginalPosition
            );
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