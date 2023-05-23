using Infrastructure.Services.Settings.Screen;
using Task = System.Threading.Tasks.Task;

namespace Infrastructure.Services.Settings
{
    public interface ISettingsService : IService
    {
        SettingsData SettingsData { get; set; }
        void SetMusicVolume(int value);
        void SetSoundVolume(int value);
        void SetGlobalVolume(int value);
        void SetQuality(int index);
        void SetResolution(ResolutionData resolution);
        void SetFullScreen(bool isFullScreen);
        void SetSensitivity(int value);
        Task Init();
    }
}