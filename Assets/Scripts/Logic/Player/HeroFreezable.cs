using System;
using Data;
using Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace Logic.Player
{
    public class HeroFreezable : MonoBehaviour, ISavedProgressWriter, IFreezable
    {
        public event Action OnFreezeChanged;

        [SerializeField] private HeroHealth _health;
        [SerializeField] private float _freezeValue;
        [SerializeField] private int _damage;
        private FreezeState _state;

        public float MaxFreeze => _state.MaxFreeze;

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
            DecreaseCurrentWarmLevel(_freezeValue);
            OnFreezeChanged?.Invoke();
            
            if (IsFroze())
                TakeDamage();
        }

        private bool IsFroze() 
            => CurrentFreeze <= 1;

        private void TakeDamage() 
            => _health.TakeDamage(_damage,false);

        public void DecreaseCurrentWarmLevel(float value) 
            => CurrentFreeze = ClampFreezeLevel(value);

        private float ClampFreezeLevel(float value) 
            => Mathf.Clamp(CurrentFreeze - value, 0, MaxFreeze);


        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.FreezeState;
            OnFreezeChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress) 
            => progress.FreezeState = _state;
    }
}