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
        private const string TemporaryProgress = "TemporaryProgress";
        private const string VerifiedProgress = "VerifiedProgress";
        private const string SettingsKey = "Settings";
        private const string GlobalProgressKey = "GlobalProgress";

        public SaveLoadService(IGameFactory gameFactory, IPersistentProgressService persistentProgressService)
        {
            _gameFactory = gameFactory;
            _persistentProgressService = persistentProgressService;
        }

        public void SavePlayerProgress()
        {
            foreach (ISavedProgressWriter progressWriter in _gameFactory.ProgressWriters)
                progressWriter.UpdateProgress(_persistentProgressService.Progress);

            PlayerPrefs.SetString(TemporaryProgress, _persistentProgressService.Progress.ToJson());
        }
        
        public PlayerProgress LoadPlayerProgress()
            => PlayerPrefs.GetString(TemporaryProgress)?.ToDeserialized<PlayerProgress>();

        public void SaveVerifiedProgress() 
            => PlayerPrefs.SetString(VerifiedProgress, _persistentProgressService.Progress.ToJson());

        public void ResetToVerified()
        {
            PlayerProgress progress = PlayerPrefs.GetString(VerifiedProgress)?.ToDeserialized<PlayerProgress>() ?? _persistentProgressService.DefaultProgress();
            _persistentProgressService.Progress = progress;
            PlayerPrefs.SetString(TemporaryProgress, progress.ToJson());
        }

        public void SaveSettings(SettingsData settingsData)
            => PlayerPrefs.SetString(SettingsKey, settingsData.ToJson());

        public SettingsData LoadSettings()
            => PlayerPrefs.GetString(SettingsKey)?.ToDeserialized<SettingsData>();

        public void SaveGlobalProgress(GlobalPlayerProgress globalProgress)
            => PlayerPrefs.SetString(GlobalProgressKey, globalProgress.ToJson());

        public GlobalPlayerProgress LoadGlobalProgress()
            => PlayerPrefs.GetString(GlobalProgressKey)?.ToDeserialized<GlobalPlayerProgress>();

        public void CleanUpPlayerProgress()
        {
            PlayerPrefs.DeleteKey(TemporaryProgress);
            PlayerPrefs.DeleteKey(VerifiedProgress);
        }
    }
}