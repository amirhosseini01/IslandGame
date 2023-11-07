using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
    public class AIReturnState : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.MovementCmp.MoveAgentByDestination(
                enemy.OriginalPosition
            );
        }

        public override void UpdateState(EnemyController enemy)
        {
            throw new System.NotImplementedException();
        }
    }
}