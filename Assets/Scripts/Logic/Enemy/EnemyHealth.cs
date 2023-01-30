using System;
using Interfaces;
using UnityEngine;

namespace Logic.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        public event Action OnHealthChanged;

        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private int _maxHealth;

        public int MaxHp
        {
            get => _maxHealth;
            set
            {
                if (value > 0) _maxHealth = value;
            }
        }

        public int CurrentHealth { get; set; }

        private void Awake()
            => CurrentHealth = _maxHealth;

        public void TakeDamage(int damage)
        {
            CurrentHealth = ClampHealthValue(damage);
            _animator.PlayTakeDamage();
            OnHealthChanged?.Invoke();
        }

        private int ClampHealthValue(int damage)
            => Mathf.Clamp(CurrentHealth - damage, 0, _maxHealth);
    }
}