using DG.Tweening;
using Logic.Animations;
using Logic.Enemy.Sound;
using Sound.SoundSystem;
using UnityEngine;

namespace Logic.Enemy
{
    public class BearAggro : EnemyAggro
    {
        [SerializeField] private SoundOperations _soundOperations;
        
        [SerializeField] private BearAnimator _animator;
        [SerializeField] private float _lookAtDuration;
        private Collider _collider;

        protected override void Aggro()
        {
            _soundOperations.PlaySound<RoarOperator>();
            
            LookAtVictim(_victimTransform)
                .OnComplete(_animator.PlayRoar);
        }
        
        private Tween LookAtVictim(Transform victim) =>
            transform.DOLookAt(victim.position, _lookAtDuration, AxisConstraint.Y, transform.up)
                .SetEase(Ease.OutExpo);

        private void OnRoared() 
            => base.Aggro();
    }
}