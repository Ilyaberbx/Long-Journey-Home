using System;
using Interfaces;
using Logic.Animations;
using UnityEngine;

namespace Logic.Player
{
    [RequireComponent(typeof(HeroHealth))]
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private HeroHealth _health;
        [SerializeField] private HeroMover _mover;
        [SerializeField] private HeroAttack _attack;
        private ICameraAnimator _animator;
        private bool _isDead;

        public void Construct(ICameraAnimator animator) => 
            _animator = animator;

        private void Awake() => 
            _health.OnHealthChanged += HealthChanged;

        private void OnDestroy() => 
            _health.OnHealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if(_isDead) return;
            
            if (_health.CurrentHealth <= 0)
                Die();
        }

        private void Die()
        {
            _isDead = true;
            _mover.enabled = false;
            _attack.enabled = false;
            _animator.PlayDeath();
        }
    }
}