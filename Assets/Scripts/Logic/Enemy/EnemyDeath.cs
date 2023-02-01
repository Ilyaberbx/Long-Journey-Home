using System;
using System.Collections;
using DG.Tweening;
using Interfaces;
using UnityEngine;

namespace Logic.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        public event Action OnDie;
        public event Action OnDisappear;

        [SerializeField] private AgentMoveToPlayer _agent;
        [SerializeField] private EnemyAttack _attack;
        [SerializeField] private EnemyAggro _aggro;
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private float _delayBeforeDestroy;

        private void Awake() => 
            _health.OnHealthChanged += HealthChanged;

        private void OnDestroy() 
            => _health.OnHealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (_health.CurrentHealth <= 0)
                Die();
        }

        private void Die()
        {
            _health.OnHealthChanged -= HealthChanged;
            _aggro.enabled = false;
            _agent.enabled = false;
            _attack.enabled = false;
            _animator.PlayDeath();
            StartCoroutine(DestroyingRoutine());
            
            OnDie?.Invoke();
        }

        private IEnumerator DestroyingRoutine()
        {
            yield return new WaitForSeconds(_delayBeforeDestroy);

            var sequence = DOTween.Sequence();
            sequence.Append(SetScaleToZero());
            sequence.AppendCallback(DestroyObject);
            sequence.AppendCallback(InvokeOnDisappear);
        }

        private void InvokeOnDisappear() =>
            OnDisappear?.Invoke();

        private Tween SetScaleToZero()
            => transform.DOScale(0, 2);

        private void DestroyObject()
            => Destroy(this);
    }
}