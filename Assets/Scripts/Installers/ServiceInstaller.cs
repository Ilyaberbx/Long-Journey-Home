using Data;
using Infrastructure.Interfaces;
using Infrastructure.Services.Achievements;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.Dialogue;
using Infrastructure.Services.EventBus;
using Infrastructure.Services.Factories;
using Infrastructure.Services.GlobalProgress;
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
            BindEventBus();
            BindInput();
            BindAssets();
            BindStaticData();
            BindPlayerProgress();
            BindGlobalProgress();
            BindAchievementService();
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

        private void BindEventBus()
            => Container.BindInterfacesTo<EventBusService>()
                .AsSingle()
                .NonLazy();

        private void BindAchievementService()
            => Container.BindInterfacesTo<AchievementService>()
                .AsSingle()
                .NonLazy();

        private void BindGlobalProgress()
            => Container.BindInterfacesTo<GlobalPlayerProgressService>()
                .AsSingle()
                .NonLazy();

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

        private void BindPlayerProgress()
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