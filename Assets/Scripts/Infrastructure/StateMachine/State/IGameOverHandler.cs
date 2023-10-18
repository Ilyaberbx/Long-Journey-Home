using Infrastructure.Services.EventBus;

namespace Infrastructure.StateMachine.State
{

    public interface IGameOverHandler : IGlobalSubscriber
    {
        void HandleGameOver();
    }
}