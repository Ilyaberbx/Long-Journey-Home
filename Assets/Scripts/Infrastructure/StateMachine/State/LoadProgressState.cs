using Infrastructure.Interfaces;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.Settings;
using Infrastructure.Services.Settings.Screen;

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

        private SettingsData SettingsByDefault()
        {
            SettingsData settings = new SettingsData();
            settings.Mouse.Sensitivity = 100;
            settings.Audio.GlobalVolume = 100;
            settings.Audio.SoundsVolume = 100;
            settings.Audio.MusicVolume = 100;
            settings.Quality.QualityIndex = 3;
            settings.Screen.IsFullScreen = true;
            settings.Screen.Resolution = new ResolutionData(1000, 500);
            return settings;
        }

        private void LoadProgressOrInitNew() 
            => _progressService.PlayerProgress = _saveLoadService.LoadProgress() 
                                                 ?? _progressService.DefaultProgress();
    }
    
}