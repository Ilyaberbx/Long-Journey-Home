using Logic.Common;
using Logic.Enemy;
using Logic.Player;
using UnityEngine;

namespace Logic.Traps
{
    public class IceTrap : MonoBehaviour
    {
        [SerializeField] private int _trapedDamage;
        [SerializeField] private IceFloor _floor;
        [SerializeField] private TriggerObserver _trapTrigger;
        [SerializeField] private TriggerObserver _deathTrigger;

        private void Awake()
        {
            _trapTrigger.OnTriggerEntered += ExecuteTrap;
            _deathTrigger.OnTriggerEntered += ApplyVictimDeath;
        }

        private void ApplyVictimDeath(Collider victim)
        {
            if (!victim.TryGetComponent(out IHealth victimHealth))
                return;
            
            victimHealth.TakeDamage(_trapedDamage);
            _floor.StopShattering();
        }

        private void OnDestroy()
        {
            _trapTrigger.OnTriggerEntered -= ExecuteTrap;
            _deathTrigger.OnTriggerEntered -= ApplyVictimDeath;
        }

        private void ExecuteTrap(Collider victim)
        {
            if (!victim.TryGetComponent(out IHealth _))
                return;

            _floor.Shatter();
        }
    }
}