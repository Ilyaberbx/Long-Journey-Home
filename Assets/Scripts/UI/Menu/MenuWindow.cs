using Infrastructure.StateMachine;
using UI.Elements;
using UI.Services.Window;
using UnityEngine;
using Zenject;

namespace UI.Menu
{
    public class MenuWindow : WindowBase
    {
        [SerializeField] private LoadLastSaveButton _lastSaveButton;
        [SerializeField] private OpenWindowButton _openWindowButton;

        [Inject]
        public void Construct(IGameStateMachine gameStateMachine,IWindowService windowService)
        {
            _lastSaveButton.Construct(gameStateMachine, _progressService);
            _openWindowButton.Construct(windowService);
        }

        protected override void Initialize()
        {
            if (FirstLoad())
                _lastSaveButton.gameObject.SetActive(false);
        }

        private bool FirstLoad()
            => _progress.IsFirstLoad;
    }
}