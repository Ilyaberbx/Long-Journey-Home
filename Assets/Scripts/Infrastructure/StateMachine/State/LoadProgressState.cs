using Infrastructure.Interfaces;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.Settings;

namespace Infrastructure.StateMachine.State
{
    public class LoadProgressState : IPayloadedState<string>
    {
        private const string MainMenuKey = "MainMenu";
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(IGameStateMachine gameStateMachine,IPersistentProgressService progressService,ISaveLoadService saveLoadService,ISettingsService settingsService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }
        
        public void Enter(string payLoad)
        {
            LoadProgressOrInitNew();

            if (payLoad == MainMenuKey) 
                _gameStateMachine.Enter<LoadMainMenuState>();
            else
                _gameStateMachine.Enter<LoadLevelState, string>(payLoad);
        }

        public void Exit()
        {}
        
        private void LoadProgressOrInitNew() 
            => _progressService.PlayerProgress = _saveLoadService.LoadProgress() 
                                                 ?? _progressService.DefaultProgress();
    }
    
}