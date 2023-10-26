using Data;
using Infrastructure.Interfaces;
using Infrastructure.Services.GlobalProgress;
using Infrastructure.Services.MusicService;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;

namespace Infrastructure.StateMachine.State
{
    public class LoadProgressState : IPayloadedState<string, AmbienceType>, IState
    {
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


        public void Enter()
        {
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<LoadMainMenuState>();
        }

        public void Enter(string scene, AmbienceType ambience)
        {
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<LoadLevelState, string, AmbienceType>(scene, ambience);
        }

        public void Exit()
        { }

        private void LoadProgressOrInitNew()
        {
            _globalProgressService.GlobalPlayerProgress = _saveLoadService.LoadGlobalProgress()
                                                          ?? new GlobalPlayerProgress();

            _progressService.Progress = _saveLoadService.LoadPlayerProgress()
                                              ?? _progressService.DefaultProgress();
        }
    }
}