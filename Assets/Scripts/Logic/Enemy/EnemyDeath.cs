﻿using System;
using System.Collections;
using DG.Tweening;
using Infrastructure.Services.Pause;
using Logic.Animations;
using Sound.SoundSystem;
using Sound.SoundSystem.Operators.Variations;
using UnityEngine;
using Zenject;

namespace Logic.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        public event Action OnDie;

        [SerializeField] private SoundOperations _soundOperations;
        [SerializeField] private Collider _hitBox;
        [SerializeField] private AgentMoveToPlayer _agent;
        [SerializeField] private EnemyAttack _attack;
        [SerializeField] private EnemyAggro _aggro;
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private float _delayBeforeDestroy;
        [SerializeField] private ParticleSystem _deathFx;
        [SerializeField] private float _fxOffSet = 5f;
        private IPauseService _pauseService;

        [Inject]
        public void Construct(IPauseService pauseService) 
            => _pauseService = pauseService;

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
            _hitBox.enabled = false;
            _attack.enabled = false;
            _aggro.enabled = false;
            _agent.Stop();
            _agent.enabled = false;
            _animator.PlayDeath();
            
            PlayDeathSound();
            UnsubscribeComponents();
            StartCoroutine(DestroyingRoutine());
            
            OnDie?.Invoke();
        }

        private void PlayDeathSound() 
            => _soundOperations.PlaySound<DeathOperator>();

        private void UnsubscribeComponents()
        {
            _pauseService.UnRegister(_agent);
            _pauseService.UnRegister(_animator);
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