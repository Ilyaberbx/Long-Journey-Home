using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Logic.Car
{
    public class CarLights : MonoBehaviour
    {
        [SerializeField] private List<Light> _frontlights;
        [SerializeField] private List<Light> _backlights;
        [SerializeField] private float _enabledLightsIntensity;
        [SerializeField] private Color _enabledLightsColor;
        [SerializeField] private Material _lightsMaterial;
        
        public Tween KickstartLights(float enableDuration, float disableDuration)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.AppendCallback(() => ApplyLightsColor(true));
            sequence.AppendCallback(() => ToggleLights(enableDuration, _enabledLightsIntensity));
            sequence.AppendInterval(enableDuration);
            sequence.AppendCallback(() => ToggleLights(disableDuration, 0f));
            sequence.AppendInterval(disableDuration);
            sequence.AppendCallback(() => ApplyLightsColor(false));
            return sequence;
        }

        public void ToggleLights(float duration, float value)
        {

            foreach (Light light in _frontlights)
               light.DOIntensity(value, duration).SetEase(Ease.InBounce);

            foreach (Light light in _backlights)
                light.DOIntensity(value, duration).SetEase(Ease.OutBounce);
        }

        private void ApplyLightsColor(bool isActive)
            => _lightsMaterial.color = isActive ? _enabledLightsColor : Color.white;
    }
}