using System;
using Data;
using Infrastructure.Interfaces;
using UnityEngine;

namespace Logic.Player
{
    public class HeroLight : MonoBehaviour, ISavedProgressWriter, IHeroLight
    {
        public event Action OnIntensityChanged;
        private FlashLightState _lightState;

        private void Start() 
            => OnIntensityChanged?.Invoke();

        public float CurrentIntensity
        {
            get => _lightState.CurrentLightIntensity;
            set
            {
                if (value < 0) return;
                OnIntensityChanged?.Invoke();
                _lightState.CurrentLightIntensity = value;
            }
        }

        public float MaxIntensity
            => _lightState.MaxLightIntensity;

        public void LoadProgress(PlayerProgress progress)
            => _lightState = progress.FlashLightState;

        public void UpdateProgress(PlayerProgress progress)
            => progress.FlashLightState = _lightState;
    }
}