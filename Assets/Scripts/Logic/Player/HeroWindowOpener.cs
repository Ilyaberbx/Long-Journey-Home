using System.Threading.Tasks;
using Data;
using Infrastructure.Services.Input;
using Infrastructure.Services.Pause;
using UI.Elements;
using UI.Envelope;
using UI.Inventory;
using UI.Pause;
using UI.Services.Window;
using UnityEngine;
using Zenject;

namespace Logic.Player
{
    public class HeroWindowOpener : MonoBehaviour, IPauseHandler, IEnvelopeOpenHandler
    {
        [SerializeField] private HeroLook _look;
        [SerializeField] private HeroCutsSceneProcessor _heroCutScene;
        [SerializeField] private HeroAttack _attack;
        [SerializeField] private HeroHudWrapper _hudWrapper;
        [SerializeField] private HeroCameraWrapper _heroCamera;

        private IInputService _input;
        private IWindowService _windowService;
        private IPauseService _pauseService;
        private WindowBase _currentWindow;


        [Inject]
        public void Construct(IInputService input, IWindowService windowService, IPauseService pauseService)
        {
            _input = input;
            _windowService = windowService;
            _pauseService = pauseService;
        }

        private async void Update()
        {
            if (IsPaused() || _heroCutScene.IsCutSceneActive)
                return;

            if (_input.IsInventoryButtonPressed())
                await OpenInventoryWindow();
        }

        public async void HandlePause(bool isPaused)
        {
            if (_currentWindow is PauseWindow)
                return;

            if (isPaused)
                _currentWindow = await OpenWindow(WindowType.Pause);
        }

        public async Task OpenEnvelopeWindow(EnvelopeData data)
        {
            if (_currentWindow is EnvelopeWindow)
                return;

            if (_currentWindow is InventoryWindow)
                return;

            EnvelopeWindow envelopeWindow = (EnvelopeWindow)await OpenWindow(WindowType.Envelope);
            envelopeWindow.UpdateContent(data.Content);

            _currentWindow = envelopeWindow;
        }

        private async Task OpenInventoryWindow()
        {
            if (_currentWindow is InventoryWindow)
                return;

            _currentWindow = await OpenWindow(WindowType.Inventory);
        }

        private async Task<WindowBase> OpenWindow(WindowType type)
        {
            CloseCurrentWindow();
            Cursor.lockState = CursorLockMode.Confined;
            _hudWrapper.Hide();
            ToggleHero(false);
            _currentWindow = await _windowService.Open(type, WindowClosed);
            return _currentWindow;
        }

        private bool IsPaused()
            => _pauseService.IsPaused;

        private void CloseCurrentWindow()
        {
            if (_currentWindow != null)
                _currentWindow.Close();
        }

        private void WindowClosed()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _currentWindow = null;

            if (_heroCutScene.IsCutSceneActive)
                return;
            
            _hudWrapper.Open();
            ToggleHero(true);
        }

        private void ToggleHero(bool value)
        {
            _attack.enabled = value;
            _look.enabled = value;
            _heroCamera.ToggleCamera(value);
        }
    }
}