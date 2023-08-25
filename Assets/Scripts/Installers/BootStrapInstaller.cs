using Infrastructure;
using Infrastructure.Services.Factories;
using Logic;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class BootStrapInstaller : MonoInstaller
    {
        [SerializeField] private LoadingCurtain _loadingCurtainPrefab;

        public override void InstallBindings()
        {
            BindLoadingCurtain();
            BindStateFactory();
        }
        
        private void BindLoadingCurtain()
        {
            LoadingCurtain curtain = Instantiate(_loadingCurtainPrefab);
            Container.Bind<LoadingCurtain>()
                .FromInstance(curtain)
                .AsSingle()
                .NonLazy();
        }

        private void BindStateFactory()
            => Container
                .Bind<IStateFactory>()
                .To<StateFactory>()
                .AsSingle()
                .NonLazy();
        
    }
}