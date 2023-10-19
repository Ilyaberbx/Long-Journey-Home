using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.State;
using Logic.Common;
using Logic.Enemy;
using Logic.Spawners;
using UnityEngine;
using Zenject;

namespace Logic.Triggers
{
    public class LevelTransferTrigger : BaseMarker
    {
        private const string PlayerTag = "Player";

        [SerializeField] private string _transferTo;
        [SerializeField] private TriggerObserver _triggerObserver;
        private IGameStateMachine _stateMachine;
        private ISaveLoadService _saveLoad;
        private IPersistentProgressService _progressService;
        private bool _triggered;

        [Inject]
        public void Construct(IGameStateMachine stateMachine,ISaveLoadService saveLoad,IPersistentProgressService progressService)
        {
            _stateMachine = stateMachine;
            _saveLoad = saveLoad;
            _progressService = progressService;
        }

        private void Awake() 
            => _triggerObserver.OnTriggerEntered += Triggered;

        private void OnDestroy() 
            => _triggerObserver.OnTriggerEntered -= Triggered;

        private void Triggered(Collider other)
        {
            if (_triggered)
                return;

            if (!other.CompareTag(PlayerTag)) return;

            _progressService.PlayerProgress.WorldData.PositionOnLevel.CurrentLevel = _transferTo;
            _saveLoad.SavePlayerProgress();
            _stateMachine.Enter<LoadProgressState, string>(_transferTo);
            _triggered = true;
        }
    }
}