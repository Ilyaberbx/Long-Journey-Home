using System;
using Data;
using DG.Tweening;
using Infrastructure.Interfaces;
using UnityEngine;

namespace Logic.Player
{
    public class FlashLight : MonoBehaviour, ISavedProgressWriter,IEquippable
    {
        public event Action OnIntensityChanged;
   
        [SerializeField] private float _lessValue;
        [SerializeField] private Light[] _lights;
        [SerializeField] private Transform[] _lightBlooms;
        [SerializeField] private float _offSet;

        private FlashLightState _flashLightState;
        private Vector3 _cachedScale;


        private void Awake() 
            => _cachedScale = transform.localScale;

        public float CurrentIntensity
        {
            get => _flashLightState.CurrentLightIntensity;
            private set
            {
                if (value < 0) return;
                _flashLightState.CurrentLightIntensity = value;
            }
        }

        public float MaxIntensity
            => _flashLightState.MaxLightIntensity;


        public void LoadProgress(PlayerProgress progress)
        {
            _flashLightState = progress.FlashLightState;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.FlashLightState.CurrentLightIntensity = CurrentIntensity;
            progress.FlashLightState.MaxLightIntensity = MaxIntensity;
        }

        public Transform GetTransform() 
            => transform;

        public void Appear()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(_cachedScale, 0.2f);
        }

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
            foreach (var light in _lights)
                light.intensity = _flashLightState.CurrentLightIntensity;
        }
    }
}