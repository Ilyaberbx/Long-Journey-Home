using System.Collections.Generic;
using Extensions;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.Settings;
using Infrastructure.Services.Settings.Screen;
using TMPro;
using UI.Elements;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace UI.Settings
{
    public class SettingsWindow : WindowBase
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Slider _globalVolume;
        [SerializeField] private Slider _musicVolume;
        [SerializeField] private Slider _soundsVolume;
        [SerializeField] private Slider _sensetivity;
        [SerializeField] private TMP_Dropdown _resolutionsDropDown;
        [SerializeField] private TMP_Dropdown _qualityDropDown;
        [SerializeField] private Toggle _fullResolutionToggle;
        private ISettingsService _settingsService;
        private ISaveLoadService _saveLoadService;
        private ResolutionData[] _resolutions;

        [Inject]
        public void Construct(ISettingsService settingsService, ISaveLoadService saveLoadService)
        {
            _settingsService = settingsService;
            _saveLoadService = saveLoadService;
        }

        protected override void OnAwake()
            => UpdateUI();

        protected override void SubscribeUpdates()
        {
            _closeButton.onClick.AddListener(Close);
            _globalVolume.onValueChanged.AddListener(SetGlobalVolume);
            _musicVolume.onValueChanged.AddListener(SetMusicVolume);
            _soundsVolume.onValueChanged.AddListener(SetSoundsVolume);
            _sensetivity.onValueChanged.AddListener(SetSensitivity);
            _qualityDropDown.onValueChanged.AddListener(SetQuality);
            _fullResolutionToggle.onValueChanged.AddListener(SetFullScreen);
            _resolutionsDropDown.onValueChanged.AddListener(SetResolution);
        }

        protected override void CleanUp()
        {
            base.CleanUp();
            _saveLoadService.SaveSettings(_settingsService.SettingsData);
        }

        private void SetSensitivity(float value)
        {
            int valueToInt = RoundToInt(value);
            _settingsService.SetSensitivity(valueToInt);
        }

        private void SetSoundsVolume(float value)
        {
            int valueToInt = Mathf.RoundToInt(value);
            _settingsService.SetSoundVolume(valueToInt);
        }

        private void SetMusicVolume(float value)
        {
            int valueToInt = Mathf.RoundToInt(value);
            _settingsService.SetMusicVolume(valueToInt);
        }

        private void SetGlobalVolume(float value)
        {
            int valueToInt = Mathf.RoundToInt(value);
            _settingsService.SetGlobalVolume(valueToInt);
        }

        private void SetQuality(int index)
            => _settingsService.SetQuality(index);

        private void SetResolution(int index)
        {
            ResolutionData selectedResolution = _resolutions[index];
            _settingsService.SetResolution(selectedResolution);
        }

        private void SetFullScreen(bool value)
            => _settingsService.SetFullScreen(value);

        private void UpdateUI()
        {
            _globalVolume.value = _settingsService.SettingsData.Audio.GlobalVolume;
            _musicVolume.value = _settingsService.SettingsData.Audio.MusicVolume;
            _soundsVolume.value = _settingsService.SettingsData.Audio.SoundsVolume;
            _sensetivity.value = _settingsService.SettingsData.Mouse.Sensitivity;
            _qualityDropDown.value = _settingsService.SettingsData.Quality.QualityIndex;
            _fullResolutionToggle.isOn = _settingsService.SettingsData.Screen.IsFullScreen;

            UpdateResolutionDropDown();
        }

        private void UpdateResolutionDropDown()
        {
            _resolutionsDropDown.ClearOptions();
            List<string> resolutionOptions = new List<string>();
            _resolutions = _settingsService.SettingsData.Screen.AvaliableResolutions;
            ResolutionData currentResolution = _settingsService.SettingsData.Screen.CurrentResolution;

            int currentResolutionIndex = 0;

            for (int i = 0; i < _resolutions.Length; i++)
            {
                resolutionOptions.Add(_resolutions[i].Width + "x" + _resolutions[i].Height);

                if (!currentResolution.IsSameResolution(_resolutions[i])) continue;
                currentResolutionIndex = i;
            }

            RefreshResolutionDropDown(currentResolutionIndex, resolutionOptions);
        }

        private void RefreshResolutionDropDown(int currentResolutionIndex, List<string> resolutionOptions)
        {
            _resolutionsDropDown.AddOptions(resolutionOptions);
            _resolutionsDropDown.value = currentResolutionIndex;
            _resolutionsDropDown.RefreshShownValue();
        }

        private int RoundToInt(float value)
            => Mathf.RoundToInt(value);
    }
}