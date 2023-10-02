using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace Logic.Vignette
{
    public class VignetteService : IVignetteService, IInitializable
    {
        private const string FreezeColorHex = "#092D79";
        private readonly VolumeProfile _profile;
        private UnityEngine.Rendering.Universal.Vignette _vignette;

        public VignetteService(VolumeProfile profile)
            => _profile = profile;

        public void Initialize()
        {
            if (!_profile.TryGet(out _vignette)) return;

            if (_vignette == null) return;

            _vignette.intensity.value = 0;
        }

        public void PlayDeath()
        {
            _vignette.color.Override(Color.red);
            _vignette.intensity.value = 1f;
        }

        public void UpdateFreeze(float currentWarmLevel, float maxWarmLevel)
            => _vignette.intensity.value = Mathf.Lerp(0, 1, 1 - currentWarmLevel / maxWarmLevel);

        public void PlayFreeze()
        {
            ColorUtility.TryParseHtmlString(FreezeColorHex, out Color freezeColor);
            _vignette.color.Override(freezeColor);
        }

        public void Reset()
        {
            _vignette.color.Override(Color.white);
            _vignette.intensity.value = 0f;
        }
    }
}