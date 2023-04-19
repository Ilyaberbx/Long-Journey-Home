using Infrastructure.Interfaces;
using Infrastructure.Services.SceneManagement;
using Logic;
using UI.Services.Factory;
using UI.Services.Window;

namespace Infrastructure.StateMachine.State
{
    public class LoadMainMenuState : IState
    {
        private const string MainMenu = "MainMenu";
        private readonly IGameStateMachine _stateMachine;
        private readonly IWindowService _windowService;
        private readonly ISceneLoader _sceneLoader;
        private readonly IUIFactory _uiFactory;
        private readonly LoadingCurtain _loadingCurtain;

        public LoadMainMenuState(IGameStateMachine stateMachine,IWindowService windowService, ISceneLoader sceneLoader,IUIFactory uiFactory, LoadingCurtain loadingCurtain)
        {
            _stateMachine = stateMachine;
            _windowService = windowService;
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter()
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(MainMenu,OnLoaded);
        }

        private async void OnLoaded()
        {
            await _uiFactory.CreateUIRoot();
            await _windowService.Open(WindowType.MainMenu);
            _loadingCurtain.Hide();
            _stateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
        }
    }
}