﻿using Cinemachine;
using Infrastructure.Services.Settings;
using Zenject;

namespace Logic.Camera
{
    public class PovGameCamera : GameCamera
    {
        private SettingsData _settings;
        private CinemachinePOV _pov;

        [Inject]
        public void Construct(ISettingsService settingsService)
            => _settings = settingsService.SettingsData;

        protected override void OnAwake()
        {
            base.OnAwake();
            _pov = Camera.GetCinemachineComponent<CinemachinePOV>();
            ApplySensitivity();
        }

        private void ApplySensitivity()
        {
            _pov.m_HorizontalAxis.m_MaxSpeed =
                _settings.Mouse.Sensitivity;

            _pov.m_VerticalAxis.m_MaxSpeed =
                _settings.Mouse.Sensitivity;
        }
    }
}