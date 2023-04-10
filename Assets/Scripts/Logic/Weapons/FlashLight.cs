using System;
using Data;
using DG.Tweening;
using Logic.Player;
using UnityEngine;

namespace Logic.Weapons
{
    public class FlashLight : BaseEquippableItem,IFlashLight
    {
        public event Action OnIntensityChanged;

        [SerializeField] private float _lessValue;
        [SerializeField] private Light[] _lights;
        [SerializeField] private Transform[] _lightBlooms;
        [SerializeField] private float _offSet;
        
        private Vector3 _cachedScale;
        private HeroLight _light;


        private void Awake()
            => _cachedScale = transform.localScale;

        public void Construct(HeroLight light) 
            => _light = light;
        

        public override void Appear()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(_cachedScale, 0.2f);
        }

        private void Update()
        {
            _light.CurrentIntensity -= _lessValue;

            DecreaseLightIntensity();
            DecreaseLightSize();

            OnIntensityChanged?.Invoke();
        }

        private void DecreaseLightSize()
        {
            for (int i = 0; i < _lightBlooms.Length; i++)
            {
                Vector3 scale = _lightBlooms[i].localScale * _light.CurrentIntensity / _light.MaxIntensity * _offSet;

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
                light.intensity = _light.CurrentIntensity;
        }
    }
}