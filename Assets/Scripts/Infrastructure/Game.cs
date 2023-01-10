using ProjectSolitude.Interfaces;
using ProjectSolitude.Logic;

namespace ProjectSolitude.Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
        {
            StateMachine = new GameStateMachine(coroutineRunner, loadingCurtain, ServiceLocator.Container);
        }
    }
}
