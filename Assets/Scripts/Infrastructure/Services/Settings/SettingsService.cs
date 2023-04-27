using System.Threading.Tasks;
using Extensions;
using Infrastructure.Services.AssetManagement;
using UnityEngine;
using UnityEngine.Audio;

namespace Infrastructure.Services.Settings
{
    public class SettingsService : ISettingsService
    {
        private const string GlobalMixer = "GlobalMixer";
        private const string MusicMixer = "MusicMixer";
        private const string SoundsMixer = "SoundsMixer";
        private const string MasterKey = "Master";
        private readonly IAssetProvider _assetProvider;
        public SettingsData SettingsData { get; set; }
        private AudioMixer _audioMixer;

        public SettingsService(IAssetProvider assetProvider) 
            => _assetProvider = assetProvider;

        public async Task Init()
        {
            _audioMixer = await _assetProvider.Load<AudioMixer>(GlobalMixer);
            RefreshQuality();
            RefreshScreen();
            SetGlobalVolume(-50);
            SetSensitivity(1000);
            SetQuality(0);
        }

        public void SetMusicVolume(int value)
        {
            SettingsData.Audio.MusicVolume = value;
        }

        public void SetSoundVolume(int value)
        {
            SettingsData.Audio.SoundsVolume = value;
        }

        public void SetGlobalVolume(int value)
        {
            SettingsData.Audio.GlobalVolume = value;
            _audioMixer.SetFloat(MasterKey, value);
        }

        public void SetQuality(int index)
        {
            SettingsData.Quality.QualityIndex = index;
            RefreshQuality();
        }

        public void SetResolution(Resolution resolution)
        {
            SettingsData.Screen.Resolution = resolution.AsResolutionData();
            RefreshScreen();
        }

        public void SetFullScreen(bool isFullScreen)
        {
            SettingsData.Screen.IsFullScreen = isFullScreen;
            RefreshScreen();
        }

        public void SetSensitivity(int value)
        {
            SettingsData.Mouse.Sensitivity = value;
            
        }


        private void RefreshQuality() 
            => QualitySettings.SetQualityLevel(SettingsData.Quality.QualityIndex);

        private void RefreshScreen()
        {
            int width = SettingsData.Screen.Resolution.Width;
            int height = SettingsData.Screen.Resolution.Width;
            bool isFullScreen = SettingsData.Screen.IsFullScreen;
            
            UnityEngine.Screen.SetResolution(width, height,
                isFullScreen);
            
            Debug.Log(UnityEngine.Screen.currentResolution);
        }
    }
}