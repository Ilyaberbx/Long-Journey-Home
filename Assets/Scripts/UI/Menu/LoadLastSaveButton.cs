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
        public void Construct(IGameStateMachine stateMachine,IPersistentProgressService progressService)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
        }

        public override void Execute()
        {
            string level = _progressService.PlayerProgress.WorldData.PositionOnLevel.Level;
            _stateMachine.Enter<LoadLevelState,string>(level);
        }
    }
}