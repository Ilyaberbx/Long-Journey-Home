using UnityEngine;

namespace Logic.Enemy
{
    [RequireComponent(typeof(EnemyAttack))]
    public class AttackCheckRange : MonoBehaviour
    {
        [SerializeField] private EnemyAttack _attack;
        [SerializeField] private TriggerObserver _triggerObserver;

        private void Start()
        {
            _triggerObserver.OnTriggerEntered += TriggerEnter;
            _triggerObserver.OnTriggerExited += TriggerExit;
            _attack.DisableAttack();
        }

        private void TriggerExit(Collider obj)
            => _attack.DisableAttack();

        private void TriggerEnter(Collider obj) 
            => _attack.EnableAttack();
    }
}