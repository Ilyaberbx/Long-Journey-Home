using Infrastructure.Services.Pause;
using Infrastructure.StateMachine;
using UI.Elements;
using UI.Menu;
using UI.Services.Window;
using UnityEngine;
using Zenject;

namespace UI.Pause
{
    public class PauseWindow : WindowBase
    {
        [SerializeField] private ResumeButton _resumeButton;
        [SerializeField] private OpenWindowButton _openWindowButton;
        [SerializeField] private MainMenuButton _menuButton;

        [Inject]
        public void Construct(IPauseService pauseService,
            IWindowService windowService,IGameStateMachine gameStateMachine)
        {
            _resumeButton.Construct(pauseService);
            _openWindowButton.Construct(windowService);
            _menuButton.Construct(gameStateMachine);
        }

        protected override void SubscribeUpdates() 
            => _resumeButton.OnResume += Close;

        protected override void CleanUp()
        {
            base.CleanUp();
            _resumeButton.OnResume -= Close;
        }
        
    }
}