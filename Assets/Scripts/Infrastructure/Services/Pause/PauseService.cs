using System.Collections.Generic;
using DG.Tweening;
using Infrastructure.Services.Input;
using UI.Services.Window;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.Pause
{
    public class PauseService : IPauseService, ITickable
    {
        private readonly IInputService _input;
        private readonly IWindowService _windowService;
        private readonly List<IPauseHandler> _handlers = new List<IPauseHandler>();
        public bool IsPaused { get; private set; }
        public bool CanBePaused { get; set; }

        public PauseService(IInputService input, IWindowService windowService)
        {
            _input = input;
            _windowService = windowService;
        }

        public void Tick()
        {
            if (!CanPause() || !CanBePaused)
                return;

            SetPaused(true);
            OpenPauseWindow();
        }

        private void OpenPauseWindow()
        {
            Cursor.lockState = CursorLockMode.Confined;
            _windowService.Open(WindowType.Pause, Resume);
        }

        private void Resume()
        {
            SetPaused(false);
            Cursor.lockState = CursorLockMode.Locked;
        }

        private bool CanPause()
            => !IsPaused && _input.IsPauseButtonPressed();

        public void Register(IPauseHandler handler)
            => _handlers.Add(handler);

        public void SetPaused(bool isPaused)
        {
            IsPaused = isPaused;

           // DOTween.timeScale = isPaused ? 0 : 1;

            foreach (IPauseHandler handler in _handlers)
                handler.HandlePause(isPaused);
        }

        public void CleanUp()
            => _handlers.Clear();

        public void UnRegister(IPauseHandler handler) 
            => _handlers.Remove(handler);
    }
}