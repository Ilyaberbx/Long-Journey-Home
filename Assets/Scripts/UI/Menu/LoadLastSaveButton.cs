using Infrastructure.Services.PersistentProgress;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.State;
using UI.Elements;
using UnityEngine;

namespace UI.Menu
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
            string level = _progressService.PlayerProgress.WorldData.PositionOnLevel.CurrentLevel;
            Debug.Log(level);
            _stateMachine.Enter<LoadLevelState,string>(level);
        }
    }
}