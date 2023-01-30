using Infrastructure.Services;
using Infrastructure.StateMachine;
using Interfaces;
using Logic;

namespace Infrastructure
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
