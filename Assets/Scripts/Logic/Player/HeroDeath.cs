using Logic.Animations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Logic.Player
{
    [RequireComponent(typeof(HeroHealth))]
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private HeroInteractor _interactor;
        [SerializeField] private HeroHealth _health;
        [SerializeField] private HeroMover _mover;
        [SerializeField] private HeroAttack _attack;
        [SerializeField] private HeroWindowOpener _windowOpener;
        [FormerlySerializedAs("_pause")] [SerializeField] private HeroPauseHandler pauseHandler;
        [SerializeField] private HeroFreeze _freeze;
        private ICameraAnimator _animator;
        private bool _isDead;

        public void SetCameraAnimator(ICameraAnimator animator) =>
            _animator = animator;

        private void Awake() =>
            _health.OnHealthChanged += HealthChanged;

        private void OnDestroy() =>
            _health.OnHealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (_isDead) return;

            if (_health.CurrentHealth <= 0)
                Die();
        }

        private void Die()
        {
            _isDead = true;
            _mover.enabled = false;
            _attack.enabled = false;
            pauseHandler.enabled = false;
            _freeze.enabled = false;
            _interactor.enabled = false;
            _windowOpener.enabled = false;
            _animator.PlayDeath();
        }
    }
}