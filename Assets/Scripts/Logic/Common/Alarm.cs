using DG.Tweening;
using Extensions;
using UnityEngine;

namespace Logic.Common
{
    public class Alarm : MonoBehaviour
    {
        [SerializeField] private float _tickRotationValue;
        [SerializeField] private float _minutesTickPerHour;
        [SerializeField] private float _tickCooldown;
        [SerializeField] private Transform _hourArrowRoot;
        [SerializeField] private Transform _minutesArrowRoot;

        private float _minutesTickPosition;

        private void Start()
            => ApplyArrowRatio();

        private void ApplyArrowRatio()
        {
            _minutesTickPosition++;

            TickArrow(_minutesArrowRoot).OnComplete(ApplyArrowRatio);;
            
            if (_minutesTickPosition >= _minutesTickPerHour)
            {
                _minutesTickPosition = 1;
                TickArrow(_hourArrowRoot);
            }
        }

        private Tween TickArrow(Transform arrow) =>
            RotateArrow(arrow).SetDelay(_tickCooldown);

        private void OnDestroy() 
            => DOTween.Kill(gameObject);

        private Tween RotateArrow(Transform arrow) 
            => arrow.DOLocalRotate(arrow.localRotation.eulerAngles + Vector3.zero.AddZ(_tickRotationValue), 0.5f);
    }
}