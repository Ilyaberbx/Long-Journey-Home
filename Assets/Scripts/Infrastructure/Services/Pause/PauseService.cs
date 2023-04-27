using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Services.Pause
{
    public class PauseService : IPauseService
    {
        private readonly List<IPauseHandler> _handlers = new List<IPauseHandler>();
        public bool IsPaused { get; private set; }

        public void Register(IPauseHandler handler)
            => _handlers.Add(handler);

        public void UnRegister(IPauseHandler handler)
            => _handlers.Remove(handler);

        public void SetPaused(bool isPaused)
        {
            IsPaused = isPaused;

            Time.timeScale = isPaused ? 0 : 1;
            
            foreach (IPauseHandler handler in _handlers)
                handler.HandlePause(isPaused);
        }

        public void CleanUp() 
            => _handlers.Clear();
    }
}