using Extensions;
using Infrastructure.Interfaces;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.Settings;
using Infrastructure.Services.Settings.Screen;
using UnityEngine;

namespace Infrastructure.StateMachine.State
{
    public class LoadProgressState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly ISettingsService _settingsService;


        public LoadProgressState(IGameStateMachine gameStateMachine,IPersistentProgressService progressService,ISaveLoadService saveLoadService,ISettingsService settingsService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _settingsService = settingsService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            LoadSettingsDataOrInitNew();
            _gameStateMachine.Enter<LoadMainMenuState>();
        }

        public void Exit()
        {
        }

        private void LoadSettingsDataOrInitNew() 
            => _settingsService.SettingsData = _saveLoadService.LoadSettings() 
                                           ?? SettingsByDefault();

        private void LoadProgressOrInitNew() 
            => _progressService.PlayerProgress = _saveLoadService.LoadProgress() 
                                                 ?? _progressService.DefaultProgress();

        private SettingsData SettingsByDefault()
        {
            SettingsData settings = new SettingsData();
            settings.Mouse.Sensitivity = 1;
            settings.Audio.GlobalVolume = -20;
            settings.Audio.SoundsVolume = -20;
            settings.Audio.MusicVolume = -20;
            settings.Quality.QualityIndex = 2;
            settings.Screen.IsFullScreen = true;
            settings.Screen.CurrentResolution = new ResolutionData(1920, 1080);
            CollectAllResolutions(settings);
            return settings;
        }

        private void CollectAllResolutions(SettingsData settings)
        {
            settings.Screen.AvaliableResolutions = new ResolutionData[Screen.resolutions.Length];
            
            for (int i = 0; i < Screen.resolutions.Length; i++)
                settings.Screen.AvaliableResolutions[i] = Screen.resolutions[i].AsResolutionData();
        }
    }
    
}