using Infrastructure.Interfaces;
using Infrastructure.Services;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.Factories;
using Infrastructure.Services.Input;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.StaticData;
using SceneManagement;
using UI.Services.Factory;
using UI.Services.Window;

namespace Infrastructure.StateMachine.State
{
    public class BootstrapState : IState
    {
        private const string Initial = "InitialScene";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ServiceLocator _serviceLocator;

        public BootstrapState(GameStateMachine stateMachine,SceneLoader sceneLoader, ServiceLocator serviceLocator)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _serviceLocator = serviceLocator;
            RegisterServices();
        }

        public void Enter() 
            => _sceneLoader.Load(Initial, EnterLoadLevel);

        public void Exit() {}
        
        private void EnterLoadLevel() 
            => _stateMachine.Enter<LoadProgressState>();
        private void RegisterServices()
        {
            RegisterInput();
            RegisterAssetsProvider();
            RegisterStaticData();
            RegisterProgress();
            RegisterUIFactory();
            RegisterWindowFactory();
            RegisterFactory();
            RegisterSaveLoadService();
        }

        private void RegisterWindowFactory() 
            => _serviceLocator.RegisterService<IWindowService>(
                new WindowService(_serviceLocator.Single<IUIFactory>()));

        private void RegisterUIFactory() 
            => _serviceLocator.RegisterService<IUIFactory>(
                new UIFactory(_serviceLocator.Single<IAssetProvider>(),
                    _serviceLocator.Single<IStaticDataService>(),_serviceLocator.Single<IPersistentProgressService>()));

        private void RegisterProgress() =>
            _serviceLocator.RegisterService<IPersistentProgressService>
                (new PersistentProgressService());

        private void RegisterAssetsProvider() 
            => _serviceLocator.RegisterService<IAssetProvider>
                (new AssetProvider());

        private void RegisterFactory() 
            => _serviceLocator.RegisterService<IGameFactory>(new GameFactory
                (_serviceLocator.Single<IAssetProvider>(),_serviceLocator.Single<IStaticDataService>(),
                    _serviceLocator.Single<IWindowService>(),_serviceLocator.Single<IInputService>()));

        private void RegisterInput()
            => _serviceLocator.RegisterService(DefineInputService());

        private void RegisterSaveLoadService() 
            => _serviceLocator.RegisterService<ISaveLoadService>(new SaveLoadService
                (_serviceLocator.Single<IGameFactory>(),_serviceLocator.Single<IPersistentProgressService>()));

        private void RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.Load();
            _serviceLocator.RegisterService(staticData);
        }

        private IInputService DefineInputService() 
            => new StandaloneInputService();
    }
}
