using Data;
using Infrastructure.Interfaces;
using Infrastructure.Services.GlobalProgress;
using Infrastructure.Services.Pause;
using Infrastructure.Services.SaveLoad;
using Logic.Player;
using UI.Services.Window;
using UnityEngine;

namespace Infrastructure.StateMachine.State
{
    public class GameEndState : IPayloadedState<HeroToggle, EndingType>
    {
        private readonly IWindowService _windowService;
        private readonly IPauseService _pauseService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IGlobalProgressService _globalProgressService;

        public GameEndState(IWindowService windowService, IPauseService pauseService, ISaveLoadService saveLoadService,
            IGlobalProgressService globalProgressService)
        {
            _windowService = windowService;
            _pauseService = pauseService;
            _saveLoadService = saveLoadService;
            _globalProgressService = globalProgressService;
        }
        
        public void Enter(HeroToggle heroToggle, EndingType ending)
        {
            Cursor.lockState = CursorLockMode.Confined;
            
            _pauseService.SetPaused(false);
            _pauseService.CanBePaused = false;

            SaveEnding(ending);
            OpenEndingWindow(ending);
            DisablePlayer(heroToggle);
            CleanPlayerProgress();
        }

        private void SaveEnding(EndingType ending)
        {
            GlobalPlayerProgress globalProgress = _globalProgressService.GlobalPlayerProgress;
            
            if (globalProgress.Endings.PassedEndings.Contains(ending))
                return;

            globalProgress.Endings.PassedEndings.Add(ending);
            _saveLoadService.SaveGlobalProgress(globalProgress);
        }

        private void OpenEndingWindow(EndingType ending)
            => _windowService.OpenEndingWindow(ending);

        private void DisablePlayer(HeroToggle heroToggle)
            => heroToggle.Toggle(false);

        private void CleanPlayerProgress()
            => _saveLoadService.CleanUpPlayerProgress();

        public void Exit()
        {
        }
    }
}