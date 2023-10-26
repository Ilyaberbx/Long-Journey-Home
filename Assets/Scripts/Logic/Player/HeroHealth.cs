﻿using System;
using Data;
using Infrastructure.Services.SaveLoad;
using Logic.Animations;
using UnityEngine;
using static UnityEngine.Mathf;

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
        
        public void Construct(ICameraAnimator animator)
            => _animator = animator;

        public void TakeDamage(int damage, bool withAnimation = true)
        {
            if (CurrentHealth <= 0)
                return;
            
            if (withAnimation)
                _animator.PlayTakeDamage();

            CurrentHealth = ClampHealthPoints(damage);
        }
        

        private int ClampHealthPoints(int damage)
            => Clamp(CurrentHealth - damage, 0, MaxHp);

        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.HealthState;
            OnHealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress) 
            => progress.HealthState = _state;
        
    }
}