using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Infrastructure.Services.Factories;
using Infrastructure.Services.MusicService;
using Infrastructure.Services.Pause;
using Infrastructure.Services.SceneManagement;
using Logic;
using UI.Services.Factory;
using Zenject;

namespace Infrastructure.StateMachine.State
{
    public class LoadNewGameState : IState
    {
        private const string IntroLevel = "Intro";
        private readonly IGameStateMachine _stateMachine;
        private readonly IUIFactory _uiFactory;
        private readonly IGameFactory _gameFactory;
        private readonly ISceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IPauseService _pauseService;
        private readonly IMusicService _musicService;

        [Inject]
        public LoadNewGameState(IGameStateMachine stateMachine
            , IUIFactory uiFactory
            , IGameFactory gameFactory
            , ISceneLoader sceneLoader, LoadingCurtain loadingCurtain,IPauseService pauseService,IMusicService musicService)
        {
            _stateMachine = stateMachine;
            _uiFactory = uiFactory;
            _gameFactory = gameFactory;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _pauseService = pauseService;
            _musicService = musicService;
        }

        public async void Enter()
        {
            _loadingCurtain.Show();
            _musicService.Stop();
            await _gameFactory.WarmUp();
            _sceneLoader.Load(IntroLevel, OnLoaded);
        }

        private async void OnLoaded()
        {
            _gameFactory.CreateContainerForCreatedObjects();
            _pauseService.CanBePaused = true;
            await InitUiRoot();
            await InitDialogueView();
            _stateMachine.Enter<GameLoopState>();
        }

        private Task InitUiRoot()
            => _uiFactory.CreateUIRoot();

        private async Task InitDialogueView()
            => await _gameFactory.CreateDialogueView();

        public void Exit()
            => _loadingCurtain.Hide();
    }
}