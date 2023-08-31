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
            string level = _progressService.PlayerProgress.WorldData.PositionOnLevel.CurrentLevel;
            _stateMachine.Enter<LoadProgressState,string>(level);
        }
    }
}