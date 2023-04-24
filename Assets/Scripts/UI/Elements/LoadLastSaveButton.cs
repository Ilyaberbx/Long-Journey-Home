using Infrastructure.Services.PersistentProgress;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.State;

namespace UI.Elements
{
    public class LoadLastSaveButton : BaseButton
    {
        private IGameStateMachine _stateMachine;
        private IPersistentProgressService _progressService;

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