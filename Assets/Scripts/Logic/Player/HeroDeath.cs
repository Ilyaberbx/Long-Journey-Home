using System.Threading.Tasks;
using Infrastructure.Services.EventBus;
using Infrastructure.Services.Pause;
using Infrastructure.Services.SaveLoad;
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
        [SerializeField] private HeroFreezable _heroFreezable;
        [SerializeField] private SoundOperations _soundOperations;
        [SerializeField] private HeroInteractor _interactor;
        [SerializeField] private HeroHealth _health;
        [SerializeField] private HeroMover _mover;
        [SerializeField] private HeroAttack _attack;
        [SerializeField] private HeroWindowOpener _windowOpener;
        [SerializeField] private HeroPauseHandler _pauseHandler;
        [SerializeField] private HeroFreezable _freezable;

        private ICameraAnimator _animator;
        private ISaveLoadService _saveLoadService;
        private IEventBusService _eventBusService;
        private bool _isDead;

        [Inject]
        public void Construct(IEventBusService eventBusService, ISaveLoadService saveLoadService)
        {
            _eventBusService = eventBusService;
            _saveLoadService = saveLoadService;
        }

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
                Die();
        }

        private void Die()
        {
            _isDead = true;
            _mover.enabled = false;
            _attack.enabled = false;
            _pauseHandler.enabled = false;
            _freezable.enabled = false;
            _interactor.enabled = false;
            _windowOpener.enabled = false;
            _animator.PlayDeath();

            if (_heroFreezable.IsFroze())
                _saveLoadService.ResetToVerified();


            PlayDeathSound();
            InformHandlers();
        }

        private void PlayDeathSound()
            => _soundOperations.PlaySound<DeathOperator>();

        private void InformHandlers()
            => _eventBusService.RaiseEvent<IGameOverHandler>(handler => handler.HandleGameOver());
    }
}