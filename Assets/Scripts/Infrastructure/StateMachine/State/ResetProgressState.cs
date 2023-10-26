using System;
using Infrastructure.Interfaces;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using Zenject;

namespace Infrastructure.StateMachine.State
{
    public class ResetProgressState : IPayloadedState<Action>
    {
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoad;
        private readonly IGameStateMachine _stateMachine;

        [Inject]
        public ResetProgressState(IPersistentProgressService progressService,ISaveLoadService saveLoad,IGameStateMachine stateMachine)
        {
            _progressService = progressService;
            _saveLoad = saveLoad;
            _stateMachine = stateMachine;
        }
        public void Enter(Action onCleaned)
        {
            _saveLoad.CleanUpPlayerProgress();
            _progressService.Progress = _progressService.DefaultProgress();
            onCleaned?.Invoke();
        }

        public void Exit()
        { }
    }
}