using System;
using Logic.Common;
using Logic.Player;
using UnityEngine;

namespace Logic.Traps
{
    public class TakeDamageOnTrigger : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private TriggerObserver _triggerObserver;

        private void Awake() 
            => _triggerObserver.OnTriggerEntered += TakeDamage;

        private void OnDestroy() 
            => _triggerObserver.OnTriggerEntered -= TakeDamage;

        private void TakeDamage(Collider victim)
        {
            if (!victim.TryGetComponent(out IHealth victimHealth))
                return;
            
            victimHealth.TakeDamage(_damage);
        }

        public void Enable() 
            => _triggerObserver.Enable();

        public void Disable() 
            => _triggerObserver.Disable();
    }
}