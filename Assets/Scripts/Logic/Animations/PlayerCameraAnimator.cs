using DG.Tweening;
using Logic.Vignette;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

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

        private Sequence _animationSequence;
        private IVignetteService _vignetteService;

        [Inject]
        public void Construct(IVignetteService vignetteService) 
            => _vignetteService = vignetteService;

        private void Awake() 
            => _animationSequence = DOTween.Sequence();

        public void PlayTakeDamage()
            => _animationSequence.Append(transform.DOShakeRotation(_takeDamageDuration, _takeDamageStrenght, 10, _takeDamageRandomness));

        public void PlayGrounded()
            => _animationSequence.Append(transform.DOShakeRotation(_groundedDuration, _groundedStrenght, 10, _groundedRandomness));

        public void PlayDeath()
            => _vignetteService.PlayDeath();
    }
}