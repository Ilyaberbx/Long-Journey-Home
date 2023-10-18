using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.EventBus;
using Infrastructure.Services.Pause;
using UI.GameOver;
using UI.Services.Window;
using UnityEngine;

namespace Infrastructure.StateMachine.State
{
    public class GameLoopState : IState, IGameOverHandler
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IWindowService _windowService;
        private readonly IPauseService _pauseService;
        private readonly IEventBusService _eventBusService;

        public GameLoopState(IGameStateMachine gameStateMachine, IAssetProvider assetProvider
            ,IWindowService windowService,IPauseService pauseService,IEventBusService eventBusService)
        {
            _assetProvider = assetProvider;
            _windowService = windowService;
            _pauseService = pauseService;
            _eventBusService = eventBusService;
        }

        public void Enter() 
            => _eventBusService.Subscribe(this);

        public void Exit()
        {
            _eventBusService.Unsubscribe(this);
            _assetProvider.CleanUp();
        }

        public async void HandleGameOver()
        {
            _pauseService.CanBePaused = false;
            Cursor.lockState = CursorLockMode.Confined;
            
            GameOverWindow gameOverWindow = await OpenGameOverWindow();
            gameOverWindow.Show();
        }

        private async Task<GameOverWindow> OpenGameOverWindow() 
            => (GameOverWindow)await _windowService.Open(WindowType.GameOver);
    }
}