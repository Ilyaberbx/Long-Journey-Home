using Logic.Camera;
using UnityEngine;
using Zenject;

namespace Installers
{

    public class LocalServiceInstaller : MonoInstaller
    {
         [SerializeField] private CameraService _camerasChangerService;

        public override void InstallBindings() 
            => BindCameraService();

        private void BindCameraService()
            => Container.BindInterfacesTo<CameraService>()
                .FromInstance(_camerasChangerService)
                .AsSingle();
    }
}