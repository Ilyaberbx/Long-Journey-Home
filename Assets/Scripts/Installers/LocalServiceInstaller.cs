using Logic.Camera;
using UnityEngine;
using Zenject;

namespace Installers
{

    public class LocalServiceInstaller : MonoInstaller
    {
         [SerializeField] private GameCamerasChangerService _camerasChangerService;

        public override void InstallBindings() 
            => BindCameraService();

        private void BindCameraService() 
            => Container.BindInterfacesTo<GameCamerasChangerService>()
                .FromInstance(_camerasChangerService)
                .AsSingle()
                .NonLazy();
    }
}