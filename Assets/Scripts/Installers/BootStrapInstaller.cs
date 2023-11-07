using Infrastructure.Services.Factories;
using Infrastructure.Services.MusicService;
using Logic;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class BootStrapInstaller : MonoInstaller
    {
        [SerializeField] private LoadingCurtain _loadingCurtainPrefab;
        [SerializeField] private AudioSource _sourcePrefab;


        public override void InstallBindings()
        {
            BindLoadingCurtain();
            BindMusicService();
            BindStateFactory();
        }

        private void BindMusicService() =>
            Container.BindInterfacesTo<MusicService>()
                .AsSingle();

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