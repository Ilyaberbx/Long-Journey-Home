using System.Collections.Generic;
using DG.Tweening;
using Logic.Inventory.Item;
using Logic.Player;
using UnityEngine;

namespace Logic.Weapons
{
    public class FlashLight : BaseEquippableItem,IFlashLight
    {
        private readonly List<float> _lightsIntensity = new List<float>();
        
        [SerializeField] private float _lessValue;
        [SerializeField] private List<Light> _lights;
        [SerializeField] private Transform[] _lightBlooms;
        [SerializeField] private float _offSet;

        private Vector3 _cachedScale;
        private IHeroLight _light;


        private void Awake()
        {
            _cachedScale = transform.localScale;

            foreach (Light light in _lights)
                _lightsIntensity.Add(light.intensity);
        }

        public void Init(IHeroLight light) 
            => _light = light;
        

        public override void Appear() 
            => transform.DOScale(_cachedScale, 0.2f);

        public override void Hide() 
            => Destroy(gameObject);

        private void Update()
        {
            _light.CurrentIntensity -= _lessValue;

            DecreaseLightIntensity();
            DecreaseLightSize();
            
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
            for (int i = 0; i < _lights.Count; i++)
                _lights[i].intensity = _lightsIntensity[i] * _light.CurrentIntensity / _light.MaxIntensity;
        }
    }
}