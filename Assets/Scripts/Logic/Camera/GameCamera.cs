using Cinemachine;
using Enums;
using Infrastructure.Services.Settings;
using UnityEngine;
using Zenject;

namespace Logic.Camera
{
    public class GameCamera : MonoBehaviour
    {
        [SerializeField] private GameCameraType _cameraType;
        private SettingsData _settings;
        private CinemachinePOV _pov;
        public GameCameraType CameraType => _cameraType;
        public CinemachineVirtualCamera Camera { get; private set; }

        [Inject]
        public void Construct(ISettingsService settingsService) 
            => _settings = settingsService.SettingsData;

        private void Awake()
        {
            Camera = GetComponent<CinemachineVirtualCamera>();
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