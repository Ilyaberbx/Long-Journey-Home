using Infrastructure.Interfaces;
using Infrastructure.Services.Pause;
using Infrastructure.Services.SceneManagement;
using Logic;
using UI.Services.Factory;
using UI.Services.Window;
using UnityEngine;

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
        private readonly IPauseService _pauseService;

        public LoadMainMenuState(IGameStateMachine stateMachine,IWindowService windowService, 
            ISceneLoader sceneLoader,IUIFactory uiFactory, 
            LoadingCurtain loadingCurtain,IPauseService pauseService)
        {
            _stateMachine = stateMachine;
            _windowService = windowService;
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _loadingCurtain = loadingCurtain;
            _pauseService = pauseService;
        }

        public void Enter()
        {
            _pauseService.CleanUp();
            _loadingCurtain.Show();
            _sceneLoader.Load(MainMenu,OnLoaded);
        }

        private async void OnLoaded()
        {
            _pauseService.CleanUp();
            _pauseService.SetPaused(false);
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