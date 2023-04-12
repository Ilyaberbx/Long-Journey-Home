using Infrastructure.Interfaces;
using Infrastructure.Services.SceneManagement;

namespace Infrastructure.StateMachine.State
{
    public class BootstrapState : IState
    {
        private const string Initial = "InitialScene";
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;

        public BootstrapState(IGameStateMachine stateMachine,ISceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter() 
            => _sceneLoader.Load(Initial, EnterLoadLevel);

        public void Exit() {}
        
        private void EnterLoadLevel() 
            => _stateMachine.Enter<LoadProgressState>();

    }
}
