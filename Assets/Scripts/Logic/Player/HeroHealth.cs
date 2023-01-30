using System;
using Data;
using Interfaces;
using ProjectSolitude.Data;
using UnityEngine;

namespace Logic.Player
{
    public class HeroHealth : MonoBehaviour, ISavedProgressWriter, IHealth
    {
        private HealthState _state;
        private ICameraAnimator _animator;
        public event Action OnHealthChanged;

        public int CurrentHealth
        {
            get => _state.CurrentHP;
            set
            {
                _state.CurrentHP = value;
                OnHealthChanged?.Invoke();
            }
        }

        public int MaxHp
        {
            get => _state.MaxHP;
            set
            {
                if (value >= 0)
                    _state.MaxHP = value;
            }
        }

        public void TakeDamage(int damage)
        {
            if (CurrentHealth <= 0)
                return;


            _animator.PlayTakeDamage();
            CurrentHealth = ClampHealthPoints(damage);
        }

        public void Construct(ICameraAnimator animator)
            => _animator = animator;

        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.HealthState;
            OnHealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HealthState.CurrentHP = CurrentHealth;
            progress.HealthState.MaxHP = MaxHp;
        }

        private int ClampHealthPoints(int damage)
            => Mathf.Clamp(CurrentHealth - damage, 0, MaxHp);
    }
}