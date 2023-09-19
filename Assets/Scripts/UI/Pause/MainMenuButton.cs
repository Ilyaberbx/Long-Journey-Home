using Infrastructure.StateMachine;
using Infrastructure.StateMachine.State;
using UI.Elements;
using Zenject;

namespace UI.Pause
{
    public class MainMenuButton : BaseButton
    {
        private const string MainMenuKey = "MainMenu";
        private IGameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(IGameStateMachine gameStateMachine)
            => _gameStateMachine = gameStateMachine;

        protected override void Execute()
        {
            DisableButton();
            _gameStateMachine.Enter<LoadProgressState, string>(MainMenuKey);
        }
    }
}