using System;
using Infrastructure.Services.Pause;
using UI.Elements;

namespace UI.Pause
{
    public class ResumeButton : BaseButton
    {
        private IPauseService _pauseService;
        public event Action OnResume;
        public void Construct(IPauseService pauseService) 
            => _pauseService = pauseService;

        public override void Execute()
        {
            _pauseService.SetPaused(false);
            OnResume?.Invoke();
        }
    }
}