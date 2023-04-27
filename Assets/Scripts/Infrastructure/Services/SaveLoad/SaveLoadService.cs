using Data;
using Extensions;
using Infrastructure.Services.Factories;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.Settings;
using UnityEngine;

namespace Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _persistentProgressService;
        private const string ProgressKey = "Progress";
        private const string SettingsKey = "Settings";

        public SaveLoadService(IGameFactory gameFactory,IPersistentProgressService persistentProgressService)
        {
            _gameFactory = gameFactory;
            _persistentProgressService = persistentProgressService;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgressWriter progressWriter in _gameFactory.ProgressWriters)
                progressWriter.UpdateProgress(_persistentProgressService.PlayerProgress);
            
            PlayerPrefs.SetString(ProgressKey,_persistentProgressService.PlayerProgress.ToJson());
        }

        public PlayerProgress LoadProgress()
            => PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();

        public SettingsData LoadSettings()
            => PlayerPrefs.GetString(SettingsKey)?.ToDeserialized<SettingsData>();

        public void SaveSettings(SettingsData settingsData)
            => PlayerPrefs.SetString(SettingsKey, settingsData.ToJson());
        

        public void CleanUp()
            => PlayerPrefs.DeleteAll();
    }
}