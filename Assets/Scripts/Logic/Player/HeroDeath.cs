using Infrastructure.Services.Pause;
using Logic.Animations;
using UI.Services.Window;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

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
        [SerializeField] private HeroPauseHandler _pauseHandler;
        [SerializeField] private HeroFreeze _freeze;
        private ICameraAnimator _animator;
        private bool _isDead;
        private IPauseService _pauseService;
        private IWindowService _windowService;

        [Inject]
        public void Construct(IPauseService pauseService, IWindowService windowService)
        {
            _windowService = windowService;
            _pauseService = pauseService;
        }

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
            Debug.Log("Die");
            _pauseService.CanBePaused = false;
            _isDead = true;
            _mover.enabled = false;
            _attack.enabled = false;
            _pauseHandler.enabled = false;
            _freeze.enabled = false;
            _interactor.enabled = false;
            _windowOpener.enabled = false;
            _animator.PlayDeath();
            Cursor.lockState = CursorLockMode.Confined;
            _windowService.Open(WindowType.GameOver);
        }
    }
}