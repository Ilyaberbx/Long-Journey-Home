using System;
using System.Collections;
using DG.Tweening;
using Logic.Animations;
using UnityEngine;

namespace Logic.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        public event Action OnDie;

        [SerializeField] private AgentMoveToPlayer _agent;
        [SerializeField] private EnemyAttack _attack;
        [SerializeField] private EnemyAggro _aggro;
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private BaseEnemyAnimator _animator;
        [SerializeField] private float _delayBeforeDestroy;
        [SerializeField] private ParticleSystem _deathFx;
        [SerializeField] private float _fxOffSet = 5f;

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
            _attack.enabled = false;
            _aggro.enabled = false;
            _agent.Stop();
            _animator.PlayDeath();
            StartCoroutine(DestroyingRoutine());

            OnDie?.Invoke();
        }

        private IEnumerator DestroyingRoutine()
        {
            yield return new WaitForSeconds(_delayBeforeDestroy);

            Sequence sequence = DOTween.Sequence();
            sequence.AppendCallback(InstantiateDeathFX);
            sequence.Append(Disappear());
            sequence.AppendCallback(DestroyObject);
        }

        private Tween Disappear()
            => transform.DOScale(Vector3.zero, 1f);

        private void InstantiateDeathFX()
            => Instantiate(_deathFx.gameObject, transform.position + Vector3.up * _fxOffSet, Quaternion.identity);

        private void DestroyObject()
            => Destroy(gameObject);
    }
}