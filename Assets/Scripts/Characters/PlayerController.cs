using Assets.Scripts.Core;
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

            EventManager.RaiseChangePlayerHealth(_healthComponent.HealthPoints);
        }
        private void Awake()
        {
            _combatComponent = this.GetComponent<Combat>();
            _healthComponent = this.GetComponent<Health>();
        }
    }
}
