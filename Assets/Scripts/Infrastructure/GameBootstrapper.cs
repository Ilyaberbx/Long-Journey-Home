using Infrastructure.Services.Factories;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.State;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private IStateFactory _stateFactory;
        private IGameStateMachine _stateMachine;
        private IGameFactory _gameFactory;

        [Inject]
        public void Construct(IStateFactory stateFactory, IGameStateMachine stateMachine,IGameFactory gameFactory)
        {
            _stateFactory = stateFactory;
            _stateMachine = stateMachine;
            _gameFactory = gameFactory;
        }

        private void Start()
            => StartGame();

        private void StartGame()
        {
            Debug.Log(gameObject);
            _stateFactory.Create(_stateMachine, typeof(BootstrapState));
            _stateFactory.Create(_stateMachine, typeof(LoadProgressState));
            _stateFactory.Create(_stateMachine, typeof(LoadSettingsState));
            _stateFactory.Create(_stateMachine, typeof(LoadMainMenuState));
            _stateFactory.Create(_stateMachine, typeof(LoadLevelState));
            _stateFactory.Create(_stateMachine, typeof(LoadNewGameState));
            _stateFactory.Create(_stateMachine, typeof(ResetProgressState));
            _stateFactory.Create(_stateMachine, typeof(GameLoopState));
            _stateFactory.Create(_stateMachine, typeof(GameEndState));

            _stateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }

        private void OnApplicationQuit() 
            => _gameFactory.CleanUp();
    }
}