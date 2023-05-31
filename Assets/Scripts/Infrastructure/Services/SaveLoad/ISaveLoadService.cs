using Data;
using Infrastructure.Services.Settings;

namespace Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
        void CleanUpProgress();
        SettingsData LoadSettings();
        void SaveSettings(SettingsData settingsData);
    }
}