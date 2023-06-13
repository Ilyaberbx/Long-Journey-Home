using Cinemachine;
using Infrastructure.Services.Settings;
using Zenject;

namespace Logic.Camera
{
    public class PovGameCamera : GameCamera
    {
        private ISettingsService _settings;
        private CinemachinePOV _pov;

        [Inject]
        public void Construct(ISettingsService settingsService)
            => _settings = settingsService;

        protected override void OnAwake()
        {
            base.OnAwake();
            _pov = Camera.GetCinemachineComponent<CinemachinePOV>();
            ApplySensitivity();
        }

        private void ApplySensitivity()
        {
            _pov.m_HorizontalAxis.m_MaxSpeed =
                _settings.SettingsData.Mouse.Sensitivity;

            _pov.m_VerticalAxis.m_MaxSpeed =
                _settings.SettingsData.Mouse.Sensitivity;
        }
    }
}