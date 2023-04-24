using Infrastructure.Interfaces;
using Infrastructure.Services.Factories;
using Infrastructure.Services.SceneManagement;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.State;
using UnityEngine;
using Zenject;

namespace Infrastructure
{

    public class GameBootstrapper : MonoBehaviour,ICoroutineRunner
    {
        private IStateFactory _stateFactory;
        private IGameStateMachine _stateMachine;
        private ISceneLoader _sceneLoader;

        [Inject]
        public void Construct(IStateFactory stateFactory,IGameStateMachine stateMachine,ISceneLoader sceneLoader)
        {
            _stateFactory = stateFactory;
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }
        private void Awake() 
            => StartGame();

        private void StartGame()
        {
            _stateFactory.Create(_stateMachine, typeof(BootstrapState));
            _stateFactory.Create(_stateMachine, typeof(LoadProgressState));
            _stateFactory.Create(_stateMachine, typeof(LoadMainMenuState));
            _stateFactory.Create(_stateMachine, typeof(LoadLevelState));
            _stateFactory.Create(_stateMachine, typeof(GameLoopState));

            _sceneLoader.Init(this);
            _stateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}