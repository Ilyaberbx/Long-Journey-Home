using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Logic.Animations
{
    public class PlayerCameraAnimator : MonoBehaviour, ICameraAnimator
    {
        [SerializeField] private VolumeProfile _profile;

        [SerializeField] private float _groundedDuration;
        [SerializeField] private float _groundedStrenght;
        [SerializeField] private int _groundedRandomness;
        [SerializeField] private float _takeDamageStrenght;
        [SerializeField] private float _takeDamageDuration;
        [SerializeField] private int _takeDamageRandomness;

        private Vignette _vignette;
        private Sequence _animationSequence = DOTween.Sequence();

        private void Awake()
        {
            if (_profile.TryGet(out Vignette vignette))
                _vignette = vignette;

            if (_vignette != null)
                _vignette.intensity.value = 0;
        }

        public void PlayTakeDamage()
            => _animationSequence.Append(transform.DOShakeRotation(_takeDamageDuration, _takeDamageStrenght, 10, _takeDamageRandomness));

        public void PlayGrounded()
            => _animationSequence.Append(transform.DOShakeRotation(_groundedDuration, _groundedStrenght, 10, _groundedRandomness));

        public void PlayDeath()
            => _vignette.intensity.value = 1f;
    }
}