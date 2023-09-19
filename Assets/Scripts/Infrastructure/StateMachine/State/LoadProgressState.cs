using Data;
using Infrastructure.Interfaces;
using Infrastructure.Services.GlobalProgress;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;

namespace Infrastructure.StateMachine.State
{
    public class LoadProgressState : IPayloadedState<string>
    {
        private const string MainMenuKey = "MainMenu";
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IGlobalProgressService _globalProgressService;

        public LoadProgressState(IGameStateMachine gameStateMachine, IPersistentProgressService progressService,
            ISaveLoadService saveLoadService, IGlobalProgressService globalProgressService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _globalProgressService = globalProgressService;
        }

        public void Enter(string scene)
        {
            LoadProgressOrInitNew();

            if (IsMenuScene(scene))
                _gameStateMachine.Enter<LoadMainMenuState>();
            else
                _gameStateMachine.Enter<LoadLevelState, string>(scene);
        }

        private bool IsMenuScene(string scene) 
            => scene == MainMenuKey;

        public void Exit()
        { }

        private void LoadProgressOrInitNew()
        {
            _globalProgressService.GlobalPlayerProgress = _saveLoadService.LoadGlobalProgress()
                                                          ?? new GlobalPlayerProgress();

            _progressService.PlayerProgress = _saveLoadService.LoadPlayerProgress()
                                              ?? _progressService.DefaultProgress();
        }
    }
}