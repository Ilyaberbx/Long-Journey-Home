using Data;
using Infrastructure.Services.Settings;

namespace Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();

        void CleanUp();
        SettingsData LoadSettings();
        void SaveSettings(SettingsData settingsData);
    }
}