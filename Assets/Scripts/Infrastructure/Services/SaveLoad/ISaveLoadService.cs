using Data;
using Infrastructure.Services.Settings;

namespace Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SavePlayerProgress();
        PlayerProgress LoadPlayerProgress();
        void SaveVerifiedProgress();
        void ResetToVerified();
        void CleanUpPlayerProgress();
        SettingsData LoadSettings();
        void SaveSettings(SettingsData settingsData);
        GlobalPlayerProgress LoadGlobalProgress();
        void SaveGlobalProgress(GlobalPlayerProgress globalProgress);
    }
}