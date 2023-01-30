using DG.Tweening;
using Interfaces;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Logic.Animations
{
    public class PlayerCameraAnimator : MonoBehaviour, ICameraAnimator
    {
        [SerializeField] private VolumeProfile _profile;
        
        [SerializeField] private float _takeDamageDuration;
        [SerializeField] private float _takeDamageStrenght;
        [SerializeField] private int _takeDamageRandomness;

        private Vignette _vignette;
        private void Awake()
        {
            if (_profile.TryGet(out Vignette vignette)) 
                _vignette = vignette;

            if (_vignette != null)
                _vignette.intensity.value = 0;
        }

        public void PlayTakeDamage() 
            => transform.DOShakeRotation(_takeDamageDuration, _takeDamageStrenght, 10, _takeDamageRandomness);

        public void PlayDeath()
            => _vignette.intensity.value = 1f;
    }
}