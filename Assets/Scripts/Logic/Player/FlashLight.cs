using System;
using Data;
using Interfaces;
using UnityEngine;

namespace Logic.Player
{
    public class FlashLight : MonoBehaviour, ISavedProgressWriter
    {
        [SerializeField] private float _lessValue;
        [SerializeField] private Light _light;
        private FlashLightState _flashLightState;

        private void Update()
        {
            // Debug.Log(_flashLightState.LightIntensity);
            _flashLightState.LightIntensity -= _lessValue;
            _light.intensity = _flashLightState.LightIntensity;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _flashLightState = progress.FlashLightState;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.FlashLightState.LightIntensity = _flashLightState.LightIntensity;
            progress.FlashLightState.MaxLightIntensity = _flashLightState.MaxLightIntensity;
        }
    }
}