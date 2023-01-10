using System;
using System.Reflection;
using ProjectSolitude.Data;
using ProjectSolitude.Interfaces;
using UnityEngine;

namespace ProjectSolitude.Logic
{
    public class HeroHealth : MonoBehaviour, ISavedProgressWriter
    {
        private HealthState _state;

        public event Action<float> OnHealthChanged;
        public float CurrentHp
        {
            get => _state.CurrentHP;
            set
            {
                if (CanChange(value))
                {
                    _state.CurrentHP = value;
                    OnHealthChanged?.Invoke(value);
                }
            }
        }

        public void TakeDamage(float damage)
        {
            if(CurrentHp <= 0)
                return;

            CurrentHp = ClampHeatlhPoints(damage);
        }

        public float MaxHp
        {
            get => _state.MaxHP;
            set
            {
                if (value >= 0)
                    _state.MaxHP = value;
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.HealthState;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HealthState.CurrentHP = CurrentHp;
            progress.HealthState.MaxHP = MaxHp;
        }

        private bool CanChange(float value) 
            => value >= 0 && _state.CurrentHP != value;

        private float ClampHeatlhPoints(float damage)
            => Mathf.Clamp(CurrentHp - damage, 0, MaxHp);
    }
}