using Infrastructure.Services.MusicService;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.State;
using UI.Elements;
using Zenject;

namespace UI.GameOver
{
    public class RestartButton : BaseButton
    {
        private IPersistentProgressService _progressService;
        private IGameStateMachine _stateMachine;

        [Inject]
        public void Construct(IGameStateMachine stateMachine, IPersistentProgressService progressService)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
        }

        protected override void Execute()
        {
            DisableButton();
            string level = _progressService.Progress.WorldData.PositionOnLevel.CurrentLevel;
            AmbienceType ambience = _progressService.Progress.AmbienceProgress.CurrentAmbience;
            _stateMachine.Enter<LoadProgressState, string, AmbienceType>(level, ambience);
        }
    }
}