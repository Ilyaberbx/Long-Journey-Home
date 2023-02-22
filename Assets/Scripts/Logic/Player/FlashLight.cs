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
        [SerializeField] private Light[] _lights;
        [SerializeField] private Transform[] _lightBlooms;
        [SerializeField] private float _offSet;

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

            DecreaseLightIntensity();
            DecreaseLightSize();
            
            OnIntensityChanged?.Invoke();
        }

        private void DecreaseLightSize()
        {
            for (int i = 0; i < _lightBlooms.Length; i++)
            {
                Vector3 scale = _lightBlooms[i].localScale * CurrentIntensity / MaxIntensity * _offSet;
                
                if (BiggerThanStartScale(i, scale)) 
                    scale = _lightBlooms[i].localScale;
                
                _lightBlooms[i].localScale = scale;
            }
        }

        private bool BiggerThanStartScale(int i, Vector3 scale) 
            => _lightBlooms[i].localScale.x < scale.x;

        private void DecreaseLightIntensity()
        {
            for (int i = 0; i < _lights.Length; i++)
                _lights[i].intensity = _flashLightState.LightIntensity;
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