using Infrastructure.Interfaces;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;

namespace Infrastructure.StateMachine.State
{
    public class LoadProgressState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        

        public LoadProgressState(IGameStateMachine gameStateMachine,IPersistentProgressService progressService,ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<LoadMainMenuState>();
        }
        public void Exit()
        {
        }

        private void LoadProgressOrInitNew() 
            => _progressService.PlayerProgress = _saveLoadService.LoadProgress() 
                                                 ?? _progressService.DefaultProgress();
    }
    
}