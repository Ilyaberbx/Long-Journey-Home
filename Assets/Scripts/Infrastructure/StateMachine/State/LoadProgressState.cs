using Data;
using Infrastructure.Interfaces;
using Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace Infrastructure.StateMachine.State
{
    public class LoadProgressState : IState
    {
        private const string MainScene = "MainScene";
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

        private PlayerProgress DefaultProgress()
        {
            PlayerProgress progress = new PlayerProgress(MainScene);

            progress.HealthState.MaxHP = 100;
            progress.HealthState.ResetHp();
            progress.FlashLightState.MaxLightIntensity = 1500;
            progress.FlashLightState.Reset();
            progress.FreezeState.MaxFreeze = 100;
            progress.FreezeState.ResetFreeze();
            progress.InventoryData.Init(30);

            return progress;
        }
    }
    
}