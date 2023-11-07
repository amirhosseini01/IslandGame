using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
    public abstract class AIBaseState
    {
        public abstract void EnterState(EnemyController enemy);
        public abstract void UpdateState(EnemyController enemy);
    }
}
