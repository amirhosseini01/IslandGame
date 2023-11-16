using UnityEngine;

namespace Assets.Scripts.Characters
{
    public class PlayerController : MonoBehaviour
    {
        public CharacterStatsScriptableObj Stats;
        private Health _healthComponent;
        private Combat _combatComponent;

        private void Start()
        {
            _healthComponent.HealthPoints = Stats.Health;
            _combatComponent.Damage = Stats.Damage;
        }
        private void Awake()
        {
            _combatComponent = GetComponent<Combat>();
            _healthComponent = GetComponent<Health>();
        }
    }
}
