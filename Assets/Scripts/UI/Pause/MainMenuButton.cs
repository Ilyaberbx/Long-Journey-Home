using Infrastructure.StateMachine;
using Infrastructure.StateMachine.State;
using UI.Elements;

namespace UI.Pause
{
    public class MainMenuButton : BaseButton
    {
        private IGameStateMachine _gameStateMachine;

        public void Construct(IGameStateMachine gameStateMachine)
            => _gameStateMachine = gameStateMachine;

        public override void Execute()
            => _gameStateMachine.Enter<LoadProgressState>();
    }
}