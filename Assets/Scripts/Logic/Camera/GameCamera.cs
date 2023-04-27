using Cinemachine;
using Enums;
using Infrastructure.Services.Settings;
using UnityEngine;

namespace Logic.Camera
{
    public class GameCamera : MonoBehaviour, ISettingsHandler
    {
        [SerializeField] private GameCameraType _cameraType;
        private SettingsData _settings;
        public GameCameraType CameraType => _cameraType;
        public CinemachineVirtualCamera Camera { get; private set; }

        private void Awake()
            => Camera = GetComponent<CinemachineVirtualCamera>();

        private void Update()
        {
            if (_settings == null)
                return;
            
            Camera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisValue *=
                _settings.Mouse.Sensitivity;
        }

        public void HandleSettings(SettingsData settings)
            => _settings = settings;
    }
}