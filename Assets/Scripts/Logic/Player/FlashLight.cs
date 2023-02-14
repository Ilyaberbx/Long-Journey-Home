using System;
using Data;
using Interfaces;
using UnityEngine;

namespace Logic.Player
{
    public class FlashLight : MonoBehaviour, ISavedProgressWriter
    {
        public event Action OnIntensityChanged;

        [SerializeField] private float _lessValue;
        [SerializeField] private Light _light;

        private FlashLightState _flashLightState;

        public float CurrentIntensity
        {
            get => _flashLightState.LightIntensity;
            private set
            {
                if (value < 0) return;
                _flashLightState.LightIntensity = value;
            }
        }

        public float MaxIntensity
            => _flashLightState.MaxLightIntensity;


        private void Update()
        {
            CurrentIntensity -= _lessValue;
            _light.intensity = _flashLightState.LightIntensity;
            OnIntensityChanged?.Invoke();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _flashLightState = progress.FlashLightState;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.FlashLightState.LightIntensity = CurrentIntensity;
            progress.FlashLightState.MaxLightIntensity = MaxIntensity;
        }
    }
}