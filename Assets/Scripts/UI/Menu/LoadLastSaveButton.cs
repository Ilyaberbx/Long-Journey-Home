using Infrastructure.Services.MusicService;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.State;
using UI.Elements;
using Zenject;

namespace UI.Menu
{
    public class LoadLastSaveButton : BaseButton
    {
        private IGameStateMachine _stateMachine;
        private IPersistentProgressService _progressService;

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
            _stateMachine.Enter<LoadLevelState, string, AmbienceType>(level, ambience);
        }
    }
}