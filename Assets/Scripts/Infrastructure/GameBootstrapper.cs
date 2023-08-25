using Infrastructure.Interfaces;
using Infrastructure.Services.Factories;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.State;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private IStateFactory _stateFactory;
        private IGameStateMachine _stateMachine;

        [Inject]
        public void Construct(IStateFactory stateFactory, IGameStateMachine stateMachine)
        {
            _stateFactory = stateFactory;
            _stateMachine = stateMachine;
        }

        private void Awake()
            => StartGame();

        private void StartGame()
        {
            _stateFactory.Create(_stateMachine, typeof(BootstrapState));
            _stateFactory.Create(_stateMachine, typeof(LoadProgressState));
            _stateFactory.Create(_stateMachine, typeof(LoadSettingsState));
            _stateFactory.Create(_stateMachine, typeof(LoadMainMenuState));
            _stateFactory.Create(_stateMachine, typeof(LoadLevelState));
            _stateFactory.Create(_stateMachine, typeof(LoadNewGameState));
            _stateFactory.Create(_stateMachine, typeof(ResetProgressState));
            _stateFactory.Create(_stateMachine, typeof(GameLoopState));

            _stateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}