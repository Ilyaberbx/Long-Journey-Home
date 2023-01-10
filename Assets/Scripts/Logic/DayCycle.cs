using System;
using UnityEngine;

namespace ProjectSolitude.Logic
{
    public class DayCycle : MonoBehaviour
    {
        [SerializeField] private Light _sun;
        [SerializeField] private Light _moon;
        
        [SerializeField] private float _dayDuration;
        
        [SerializeField] private AnimationCurve _sunCurve;
        [SerializeField] private AnimationCurve _moonCurve;
        [SerializeField] private AnimationCurve _skyCurve;
        
        [SerializeField] private Material _daySkyBox;
        [SerializeField] private Material _nightSkyBox;
        
        [SerializeField] private ParticleSystem _stars;
        
        private float _sunIntensity;
        private float _moonIntensity;
        private float _timeOfDay;

        private void Awake()
        {
            _sunIntensity = _sun.intensity;
            _moonIntensity = _moon.intensity;
        }

        private void Update()
        {
            _timeOfDay += Time.deltaTime / _dayDuration;
            
            if (_timeOfDay > 1) _timeOfDay = 0;

            RotateSun();
            RotateMoon();

            DefineStars();
            DefineSkyBox();
            DefineSunIntensity();
            DefineMoonIntensity();
        }

        private void DefineStars()
        {
            var mainModule = _stars.main;
            mainModule.startColor = new Color(1, 1, 1, 1 - _skyCurve.Evaluate(_timeOfDay));
        }

        private void DefineSkyBox()
        {
            RenderSettings.skybox.Lerp(_nightSkyBox, _daySkyBox, _skyCurve.Evaluate(_timeOfDay));
            RenderSettings.sun = _skyCurve.Evaluate(_timeOfDay) > 0.1f ? _sun : _moon;
            DynamicGI.UpdateEnvironment();
        }
        private void RotateSun() => _sun.transform.localRotation = Quaternion.Euler(_timeOfDay * 360f, 180, 0);
        private void RotateMoon() => _moon.transform.localRotation = Quaternion.Euler(_timeOfDay * 360f + 180, 180, 0);
        private void DefineSunIntensity() => _sun.intensity = _sunIntensity * _sunCurve.Evaluate(_timeOfDay);
        private void DefineMoonIntensity() => _moon.intensity = _moonIntensity * _moonCurve.Evaluate(_timeOfDay);
    }
}