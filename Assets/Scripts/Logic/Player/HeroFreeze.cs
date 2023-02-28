using System;
using Data;
using Interfaces;
using UnityEngine;

namespace Logic.Player
{
    public class HeroFreeze : MonoBehaviour, ISavedProgressWriter, IFreeze
    {
        public event Action OnFreezeChanged;

        [SerializeField] private HeroHealth _health;
        [SerializeField] private float _freezeValue;
        [SerializeField] private int _damage;
        private FreezeState _state;

        public float MaxFreeze
        {
            get => _state.MaxFreeze;
            set
            {
                if (value > 0)
                    _state.MaxFreeze = value;
            }
        }
        public float CurrentFreeze
        {
            get => _state.CurrentFreeze;
            set
            {
                if (value > 0)
                    _state.CurrentFreeze = value;
            }
        }

        private void Update()
        {
            DecreaseCurrentWarmLevel();
            OnFreezeChanged?.Invoke();
            
            if (IsFroze())
                TakeDamage();
        }

        private bool IsFroze() 
            => CurrentFreeze <= 1;

        private void TakeDamage() 
            => _health.TakeDamage(_damage,false);

        private void DecreaseCurrentWarmLevel() 
            => CurrentFreeze = ClampFreezeLevel();

        private float ClampFreezeLevel() 
            => Mathf.Clamp(CurrentFreeze - _freezeValue, 0, MaxFreeze);


        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.FreezeState;
            OnFreezeChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress) => progress.FreezeState = _state;
    }
}