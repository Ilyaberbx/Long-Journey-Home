using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.Pause;
using Infrastructure.Services.SceneManagement;
using Infrastructure.Services.StaticData;

namespace Infrastructure.StateMachine.State
{
    public class BootstrapState : IState
    {
        private const string MainMenu = "MainMenu";
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;
        private readonly IPauseService _pause;

        public BootstrapState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, IAssetProvider assetProvider,
            IStaticDataService staticData,IPauseService pause)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _assetProvider = assetProvider;
            _staticData = staticData;
            _pause = pause;
        }

        public async void Enter()
        {
            await PreWarmUp();
            _pause.CanBePaused = false;
            _sceneLoader.Load(MainMenu, EnterLoadLevel);
        }

        public void Exit()
        {}

        private async Task PreWarmUp()
        {
            await _assetProvider.Initialize();
            await _staticData.Load();
        }

        private void EnterLoadLevel()
            => _stateMachine.Enter<LoadSettingsState>();
    }
}