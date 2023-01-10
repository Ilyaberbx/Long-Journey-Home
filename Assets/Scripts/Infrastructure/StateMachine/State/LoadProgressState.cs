using ProjectSolitude.Data;
using ProjectSolitude.Infrastructure.PersistentProgress;
using ProjectSolitude.Interfaces;

namespace ProjectSolitude.Infrastructure
{
    public class LoadProgressState : IState
    {
        private const string MainScene = "MainScene";
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        

        public LoadProgressState(GameStateMachine gameStateMachine,IPersistentProgressService progressService,ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            var level = _progressService.PlayerProgress.WorldData.PositionOnLevel.Level;
            _gameStateMachine.Enter<LoadLevelState,string>(level);
        }
        public void Exit()
        {
            
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.PlayerProgress = _saveLoadService.LoadProgress() 
                                              ?? DefaultProgress();
        }

        private static PlayerProgress DefaultProgress()
        {
            PlayerProgress progress = new PlayerProgress(MainScene);

            progress.HealthState.MaxHP = 30f;
            progress.HealthState.ResetHp();

            return progress;
        }
    }
    
}