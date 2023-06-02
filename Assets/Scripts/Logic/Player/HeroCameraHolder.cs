using Cinemachine;
using Infrastructure.Services.Settings;
using UnityEngine;
using Zenject;

namespace Logic.Player
{

    public class HeroCameraHolder : MonoBehaviour
    {
        public Transform PovPoint => _povPoint;
        public CinemachinePOV Camera => _cameraPov;
        [SerializeField] private Transform _povPoint;
        private CinemachinePOV _cameraPov;
        private ISettingsService _settings;

        [Inject]
        public void Construct(ISettingsService settings) 
            => _settings = settings;

        public void Init(CinemachinePOV cameraPov)
            => _cameraPov = cameraPov;

        public void ToggleCamera(bool value)
        {
            _cameraPov.m_HorizontalAxis.m_MaxSpeed = value ? _settings.SettingsData.Mouse.Sensitivity : 0;
            _cameraPov.m_VerticalAxis.m_MaxSpeed = value ? _settings.SettingsData.Mouse.Sensitivity : 0;
        }
    }
}