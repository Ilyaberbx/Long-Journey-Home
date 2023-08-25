using Infrastructure.Interfaces;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.Dialogue;
using Infrastructure.Services.Factories;
using Infrastructure.Services.Input;
using Infrastructure.Services.Pause;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.SceneManagement;
using Infrastructure.Services.Settings;
using Infrastructure.Services.StaticData;
using Infrastructure.StateMachine;
using UI.Services.Factory;
using UI.Services.Window;
using Zenject;

namespace Installers
{
    public class ServiceInstaller : MonoInstaller, ICoroutineRunner
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
            BindPause();
            BindSceneLoader();
            BindStateMachine();
            BindSettings();
            BindDialogueService();
        }

        private void BindDialogueService()
            => Container.Bind<IDialogueService>()
                .To<DialogueService>().AsSingle()
                .WithArguments(this)
                .NonLazy();

        private void BindSettings()
            => Container.Bind<ISettingsService>()
                .To<SettingsService>()
                .AsSingle()
                .NonLazy();

        private void BindSceneLoader()
            => Container.Bind<ISceneLoader>()
                .To<SceneLoader>()
                .AsSingle()
                .NonLazy();

        private void BindPause()
            => Container.BindInterfacesTo<PauseService>()
                .AsSingle()
                .NonLazy();


        private void BindStateMachine()
            => Container.Bind<IGameStateMachine>()
                .To<GameStateMachine>()
                .AsSingle()
                .NonLazy();

        private void BindSaveLoad()
            => Container.Bind<ISaveLoadService>()
                .To<SaveLoadService>()
                .AsSingle()
                .NonLazy();

        private void BindGameFactory()
            => Container.Bind<IGameFactory>()
                .To<GameFactory>()
                .AsSingle()
                .NonLazy();

        private void BindWindowService()
            => Container.Bind<IWindowService>()
                .To<WindowService>()
                .AsSingle()
                .NonLazy();

        private void BindUIFactory()
            => Container.Bind<IUIFactory>()
                .To<UIFactory>()
                .AsSingle()
                .NonLazy();

        private void BindProgress()
            => Container.Bind<IPersistentProgressService>()
                .To<PersistentProgressService>()
                .AsSingle()
                .NonLazy();

        private void BindStaticData()
            => Container.Bind<IStaticDataService>()
                .To<StaticDataService>()
                .AsSingle()
                .NonLazy();

        private void BindAssets()
            => Container.Bind<IAssetProvider>()
                .To<AssetProvider>()
                .AsSingle()
                .NonLazy();


        private void BindInput()
            => Container.Bind<IInputService>()
                .To<StandaloneInputService>()
                .AsSingle()
                .NonLazy();
    }
}