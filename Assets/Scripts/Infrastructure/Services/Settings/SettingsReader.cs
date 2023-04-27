namespace Infrastructure.Services.Settings
{
    public interface ISettingsHandler
    {
        void HandleSettings(SettingsData settings);
    }
}