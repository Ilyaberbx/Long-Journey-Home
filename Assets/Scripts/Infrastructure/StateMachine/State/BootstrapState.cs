using ProjectSolitude.Infrastructure.AssetManagment;
using ProjectSolitude.Infrastructure.PersistentProgress;
using ProjectSolitude.Infrastructure.SaveLoad;
using ProjectSolitude.Infrastructure.SceneManagment;
using ProjectSolitude.Inputs;
using ProjectSolitude.Interfaces;
using UnityEngine;

namespace ProjectSolitude.Infrastructure
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
        {
            _sceneLoader.Load(Initial, EnterLoadLevel);
        }

        public void Exit() {}
        
        private void EnterLoadLevel() 
            => _stateMachine.Enter<LoadProgressState>();
        private void RegisterServices()
        {
            RegisterInput();
            RegisterAssetsProvider();
            RegisterFactory();
            RegisterProgress();
            RegisterSaveLoadService();
        }

        private void RegisterProgress() =>
            _serviceLocator.RegisterService<IPersistentProgressService>
                (new PersistentProgressService());

        private void RegisterAssetsProvider() 
            => _serviceLocator.RegisterService<IAssetProvider>
                (new AssetProvider());

        private void RegisterFactory() 
            => _serviceLocator.RegisterService<IGameFactory>(new GameFactory
                (_serviceLocator.Single<IAssetProvider>()));

        private void RegisterInput()
            => _serviceLocator.RegisterService(DefineInputService());

        private void RegisterSaveLoadService() 
            => _serviceLocator.RegisterService<ISaveLoadService>(new SaveLoadService
                (_serviceLocator.Single<IGameFactory>(),_serviceLocator.Single<IPersistentProgressService>()));

        private IInputService DefineInputService() 
            => new StandaloneInputService();
    }
}
