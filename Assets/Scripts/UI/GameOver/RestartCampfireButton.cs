using Infrastructure.Services.MusicService;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.State;
using UI.Elements;
using UnityEngine;
using Zenject;

namespace UI.GameOver
{

    public class RestartCampfireButton : BaseButton
    {
        private IGameStateMachine _stateMachine;
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        [Inject]
        public void Construct(IPersistentProgressService progressService,IGameStateMachine stateMachine,ISaveLoadService saveLoadService)
        {
            _progressService = progressService;
            _stateMachine = stateMachine;
            _saveLoadService = saveLoadService;
        }
        protected override void Execute()
        {
            DisableButton();
            _saveLoadService.ResetToVerified();
            string level = _progressService.Progress.WorldData.PositionOnLevel.CurrentLevel;
            AmbienceType ambience = _progressService.Progress.AmbienceProgress.CurrentAmbience;
            _stateMachine.Enter<LoadProgressState, string, AmbienceType>(level, ambience);
        }
    }
}