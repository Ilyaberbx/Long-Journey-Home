using Infrastructure.Services.MusicService;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.State;
using Logic.Common;
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
        [SerializeField] private AmbienceType _ambienceType;
        private IGameStateMachine _stateMachine;
        private ISaveLoadService _saveLoad;
        private IPersistentProgressService _progressService;
        private bool _triggered;

        [Inject]
        public void Construct(IGameStateMachine stateMachine, ISaveLoadService saveLoad,
            IPersistentProgressService progressService)
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

            SaveProgress();

            _stateMachine.Enter<LoadProgressState, string, AmbienceType>(_transferTo, _ambienceType);
            _triggered = true;
        }

        private void SaveProgress()
        {
            _progressService.Progress.WorldData.PositionOnLevel.CurrentLevel = _transferTo;
            _progressService.Progress.AmbienceProgress.CurrentAmbience = _ambienceType;
            
            _saveLoad.SavePlayerProgress();
        }
    }
}