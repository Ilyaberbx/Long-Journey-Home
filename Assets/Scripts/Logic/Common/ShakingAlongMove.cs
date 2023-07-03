using DG.Tweening;
using Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace Logic.Common
{
    public class ShakingAloneMove : MonoBehaviour
    {
        [SerializeField] private float _shakeCoeficient;
        private IInputService _input;
        private Tween _shakingTween;
        private Tween _returningTween;
        private Vector3 _cachedPosition;
        private bool _isShaking;

        [Inject]
        public void Construct(IInputService inputService) 
            => _input = inputService;

        private void Start() 
            => _cachedPosition = transform.localPosition;

        private void Update()
        {
            if (IsMoving())
            {
                if (_isShaking) return;
                _returningTween?.Kill();
                Shake();
            }
            else if(_isShaking)
            {
                DOTween.Kill(gameObject);
                _returningTween = transform.DOLocalMove(_cachedPosition, 0.5f).OnComplete(Shaked);
            }
        }

        private void Shake()
        {
            _isShaking = true;
            _shakingTween = transform.DOShakePosition(1, _shakeCoeficient, 0,10f).OnComplete(Shaked);
        }

        private void Shaked() 
            => _isShaking = false;

        private bool IsMoving() 
            => _input.Horizontal >= 1 || _input.Vertical >= 1;
    }
}