using System;
using Data;
using Infrastructure.Services.SaveLoad;
using Logic.Vignette;
using UnityEngine;
using Zenject;

namespace Logic.Player
{
    public class HeroFreezable : MonoBehaviour, ISavedProgressWriter, IFreezable
    {
        public event Action OnFreezeChanged;

        [SerializeField] private HeroHealth _health;
        [SerializeField] private float _freezeValue;
        [SerializeField] private int _damage;
        private FreezeState _state;
        private IVignetteService _vignetteService;

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

        [Inject]
        public void Construct(IVignetteService vignetteService) 
            => _vignetteService = vignetteService;

        private void Start() 
            => _vignetteService.PlayFreeze();

        private void Update()
        {
            DecreaseCurrentWarmLevel(_freezeValue);
            OnFreezeChanged?.Invoke();
            UpdateVignette();
            
            if (IsFroze())
                TakeDamage();
        }

        private void UpdateVignette() 
            => _vignetteService.UpdateFreeze(_state.CurrentFreeze, _state.MaxFreeze);

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