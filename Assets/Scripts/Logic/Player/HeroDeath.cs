using System.Threading.Tasks;
using Infrastructure.Services.EventBus;
using Infrastructure.Services.Pause;
using Infrastructure.StateMachine.State;
using Logic.Animations;
using Sound.SoundSystem;
using Sound.SoundSystem.Operators;
using Sound.SoundSystem.Operators.Variations;
using UI.Elements;
using UI.GameOver;
using UI.Services.Window;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Logic.Player
{
    [RequireComponent(typeof(HeroHealth))]
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private SoundOperations _soundOperations;
        [SerializeField] private HeroInteractor _interactor;
        [SerializeField] private HeroHealth _health;
        [SerializeField] private HeroMover _mover;
        [SerializeField] private HeroAttack _attack;
        [SerializeField] private HeroWindowOpener _windowOpener;
        [SerializeField] private HeroPauseHandler _pauseHandler;
        [SerializeField] private HeroFreezable _freezable;

        private ICameraAnimator _animator;
        private bool _isDead;
        private IEventBusService _eventBusService;

        [Inject]
        public void Construct(IEventBusService eventBusService)
            => _eventBusService = eventBusService;

        public void SetCameraAnimator(ICameraAnimator animator) =>
            _animator = animator;

        private void Awake() =>
            _health.OnHealthChanged += HealthChanged;

        private void OnDestroy() =>
            _health.OnHealthChanged -= HealthChanged;

        private async void HealthChanged()
        {
            if (_isDead) return;

            if (_health.CurrentHealth <= 0)
                await Die();
        }

        private async Task Die()
        {
            _isDead = true;
            _mover.enabled = false;
            _attack.enabled = false;
            _pauseHandler.enabled = false;
            _freezable.enabled = false;
            _interactor.enabled = false;
            _windowOpener.enabled = false;
            PlayDeathSound();
            _animator.PlayDeath();
            InformHandlers();
        }

        private void PlayDeathSound()
            => _soundOperations.PlaySound<DeathOperator>();

        private void InformHandlers()
            => _eventBusService.RaiseEvent<IGameOverHandler>(handler => handler.HandleGameOver());
    }
}