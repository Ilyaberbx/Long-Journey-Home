using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.Factories;
using Infrastructure.Services.Input;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.StaticData;
using Infrastructure.StateMachine;
using UI.Services.Factory;
using UI.Services.Window;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu]
    public class ServiceInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            BindInput();
            BindAssets();
            BindStaticData();
            BindProgress();
            BindUIFactory();
            BindWindowService();
            BindGameFactory();
            BindSaveLoad();
            BindStateMachine();
            
        }
        
        

        private void BindStateMachine() 
            => Container.Bind<IGameStateMachine>()
                .To<GameStateMachine>()
                .FromNew()
                .AsSingle()
                .NonLazy();

        private void BindSaveLoad() 
            => Container.Bind<ISaveLoadService>()
                .To<SaveLoadService>()
                .FromNew()
                .AsSingle()
                .NonLazy();

        private void BindGameFactory()
            => Container.Bind<IGameFactory>()
                .To<GameFactory>()
                .FromNew()
                .AsSingle()
                .NonLazy();

        private void BindWindowService()
            => Container.Bind<IWindowService>()
                .To<WindowService>()
                .FromNew()
                .AsSingle()
                .NonLazy();

        private void BindUIFactory()
            =>
                Container.Bind<IUIFactory>()
                    .To<UIFactory>()
                    .FromNew()
                    .AsSingle()
                    .NonLazy();

        private void BindProgress()
            => Container.Bind<IPersistentProgressService>()
                .To<PersistentProgressService>()
                .FromNew()
                .AsSingle()
                .NonLazy();

        private void BindStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.Load();
            Container.Bind<IStaticDataService>()
                .FromInstance(staticData)
                .AsSingle()
                .NonLazy();
        }

        private void BindAssets()
            => Container.Bind<IAssetProvider>()
                .To<AssetProvider>()
                .FromNew()
                .AsSingle()
                .NonLazy();


        private void BindInput()
            => Container.Bind<IInputService>()
                .To<StandaloneInputService>()
                .FromNew()
                .AsSingle()
                .NonLazy();
    }
}