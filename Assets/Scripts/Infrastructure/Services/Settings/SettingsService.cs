using System.Threading.Tasks;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.Settings.Screen;
using UnityEngine;
using UnityEngine.Audio;

namespace Infrastructure.Services.Settings
{
    public class SettingsService : ISettingsService
    {
        private const string GlobalMixer = "GlobalMixer";
        private const string MasterKey = "MasterVolume";
        private const string SoundKey = "SoundVolume";
        private const string MusicKey = "MusicVolume";
        private readonly IAssetProvider _assetProvider;
        public SettingsData SettingsData { get; set; }
        private AudioMixer _audioMixer;

        public SettingsService(IAssetProvider assetProvider)
            => _assetProvider = assetProvider;

        public async Task Init()
        {
            _audioMixer = await _assetProvider.Load<AudioMixer>(GlobalMixer);
            Debug.Log(_audioMixer);
            RefreshAllSettings();
        }

        public void SetMusicVolume(int value)
        {
            SettingsData.Audio.MusicVolume = value;
            RefreshMusicVolume();
        }

        public void SetSoundVolume(int value)
        {
            SettingsData.Audio.SoundsVolume = value;
            RefreshSoundsVolume();
        }

        public void SetGlobalVolume(int value)
        {
            SettingsData.Audio.GlobalVolume = value;
            RefreshGlobalVolume();
        }

        public void SetQuality(int index)
        {
            SettingsData.Quality.QualityIndex = index;
            RefreshQuality();
        }

        public void SetResolution(ResolutionData resolution)
        {
            SettingsData.Screen.CurrentResolution = resolution;
            RefreshScreen();
        }

        public void SetFullScreen(bool isFullScreen)
        {
            SettingsData.Screen.IsFullScreen = isFullScreen;
            RefreshScreen();
        }

        public void SetSensitivity(float value)
            => SettingsData.Mouse.Sensitivity = value;

        private void RefreshSoundsVolume() 
            => _audioMixer.SetFloat(SoundKey, SettingsData.Audio.SoundsVolume);

        private void RefreshGlobalVolume()
        {
            Debug.Log("Refresh");
            _audioMixer.SetFloat(MasterKey, SettingsData.Audio.GlobalVolume);
        }

        private void RefreshMusicVolume() 
            => _audioMixer.SetFloat(MusicKey, SettingsData.Audio.MusicVolume);


        private void RefreshQuality()
            => QualitySettings.SetQualityLevel(SettingsData.Quality.QualityIndex);

        private void RefreshScreen()
        {
            int width = SettingsData.Screen.CurrentResolution.Width;
            int height = SettingsData.Screen.CurrentResolution.Width;
            bool isFullScreen = SettingsData.Screen.IsFullScreen;

            UnityEngine.Screen.SetResolution(width, height,
                isFullScreen);
        }

        private void RefreshAllSettings()
        {
            RefreshScreen();
            RefreshQuality();
            RefreshGlobalVolume();
            RefreshMusicVolume();
            RefreshSoundsVolume();
        }
    }
}