using System.Collections.Generic;
using DG.Tweening;
using Logic.Inventory.Item;
using Logic.Player;
using Sound.SoundSystem;
using Sound.SoundSystem.Operators;
using UnityEngine;

namespace Logic.Weapons
{
    public class FlashLight : BaseEquippableItem, IFlashLight
    {
        private const int MinIntensityValue = 10;
        private readonly List<float> _lightsIntensity = new List<float>();

        [SerializeField] private SoundOperations _soundOperations;
        [SerializeField] private float _lessValue;
        [SerializeField] private List<Light> _lights;
        [SerializeField] private Transform[] _lightBlooms;
        [SerializeField] private float _offSet;

        private Vector3 _cachedScale;
        private IHeroLight _light;
        private bool _isFlaming;

        private void Awake()
        {
            _cachedScale = transform.localScale;

            foreach (Light light in _lights)
                _lightsIntensity.Add(light.intensity);
        }

        public void Init(IHeroLight light)
            => _light = light;

        public override void Appear()
        {
            transform.DOScale(_cachedScale, 0.2f);
            
            if (NoLightIntensity())
                return;
            
            _soundOperations.PlaySound<LoopSoundOperator>();
        }

        public override void Hide()
        {
            _soundOperations.Stop();
            Destroy(gameObject);
        }

        private void Update()
        {
            _light.CurrentIntensity -= _lessValue;
            
            DecreaseLightIntensity();
            DecreaseLightSize();

            if (NoLightIntensity() && _isFlaming)
            {
                _isFlaming = false;
                _soundOperations.Stop();
                return;
            }

            if (_isFlaming || NoLightIntensity()) return;

            _isFlaming = true;
            _soundOperations.PlaySound<LoopSoundOperator>();
        }

        private bool NoLightIntensity()
            => _light.CurrentIntensity <= MinIntensityValue;

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
            for (int i = 0; i < _lights.Count; i++)
                _lights[i].intensity = _lightsIntensity[i] * _light.CurrentIntensity / _light.MaxIntensity;
        }
    }
}