using Infrastructure.Services.Pause;
using Infrastructure.StateMachine;
using UI.Elements;
using UI.Menu;
using UI.Services.Window;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Pause
{
    public class PauseWindow : WindowBase
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private OpenWindowButton _openWindowButton;
        [SerializeField] private MainMenuButton _menuButton;

        [Inject]
        public void Construct(IPauseService pauseService,
            IWindowService windowService,IGameStateMachine gameStateMachine)
        {
            _openWindowButton.Construct(windowService);
            _menuButton.Construct(gameStateMachine);
        }

        protected override void SubscribeUpdates()
            => _closeButton.onClick.AddListener(Close);

        protected override void CleanUp()
        {
            base.CleanUp();
            _closeButton.onClick.RemoveListener(Close);
        }
        
    }
}