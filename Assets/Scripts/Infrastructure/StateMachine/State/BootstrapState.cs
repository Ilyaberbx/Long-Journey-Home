using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.Factories;
using Infrastructure.Services.Hint;
using Infrastructure.Services.Pause;
using Infrastructure.Services.SceneManagement;
using Infrastructure.Services.StaticData;
using UI.Elements;

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
        private readonly IGameFactory _gameFactory;
        private readonly IHintService _hintService;

        public BootstrapState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, IAssetProvider assetProvider,
            IStaticDataService staticData,IPauseService pause,IGameFactory gameFactory,IHintService hintService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _assetProvider = assetProvider;
            _staticData = staticData;
            _pause = pause;
            _gameFactory = gameFactory;
            _hintService = hintService;
        }

        public async void Enter()
        {
            await PreWarmUp();
            _pause.CanBePaused = false;
            await InitializeGlobalView();
            _hintService.ShowHint("Movement: WASD,Space");
            _hintService.ShowHint("Inventory: I");
            _sceneLoader.Load(MainMenu, OnMainMenuLoaded);
        }

        private Task<HintView> InitializeGlobalView() 
            => _gameFactory.CreateHintView();

        public void Exit()
        {}

        private async Task PreWarmUp()
        {
            await _assetProvider.Initialize();
            await _staticData.Load();
        }

        private void OnMainMenuLoaded()
            => _stateMachine.Enter<LoadSettingsState>();
    }
}