namespace Assets.Scripts.Characters
{
	public abstract class AiBaseState
    {
        public abstract void EnterState(EnemyController enemy);
        public abstract void UpdateState(EnemyController enemy);
    }
}
