using Cinemachine;
using Infrastructure.Services.Settings;
using Logic.Camera;
using UnityEngine;
using Zenject;

namespace Logic.Player
{

    public class HeroCameraWrapper : MonoBehaviour
    {
        [SerializeField] private HeroEquiper _equiper;
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
        

        public void ParentEquipmentToMainCamera()
        {
            if (UnityEngine.Camera.main != null)
                _equiper.EquipmentContainer.SetParent(UnityEngine.Camera.main.transform);
        }
    }
}