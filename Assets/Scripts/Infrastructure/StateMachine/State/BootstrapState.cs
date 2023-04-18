using Infrastructure.Interfaces;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.SceneManagement;
using Infrastructure.Services.StaticData;

namespace Infrastructure.StateMachine.State
{
    public class BootstrapState : IState
    {
        private const string Initial = "InitialScene";
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;

        public BootstrapState(IGameStateMachine stateMachine,ISceneLoader sceneLoader,IAssetProvider assetProvider,IStaticDataService staticData)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _assetProvider = assetProvider;
            _staticData = staticData;
        }

        public void Enter()
        {
            PreWarmUp();
            _sceneLoader.Load(Initial, EnterLoadLevel);
        }

        private void PreWarmUp()
        {
            _assetProvider.Initialize();
            _staticData.Load();
        }

        public void Exit() {}
        
        private void EnterLoadLevel() 
            => _stateMachine.Enter<LoadProgressState>();

    }
}
