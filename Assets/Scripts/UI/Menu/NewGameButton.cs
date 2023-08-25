using System;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.State;
using UI.Elements;
using Zenject;

namespace UI.Menu
{
    public class NewGameButton : BaseButton
    {
        private IGameStateMachine _stateMachine;

        [Inject]
        public void Construct(IGameStateMachine stateMachine) 
            => _stateMachine = stateMachine;

        public override void Execute() 
            => _stateMachine.Enter<ResetProgressState, Action>(LoadNewGame);

        private void LoadNewGame() 
            => _stateMachine.Enter<LoadNewGameState>();
    }
}