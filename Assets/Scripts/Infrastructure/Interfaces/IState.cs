namespace Infrastructure.Interfaces
{
    public interface IState : IExitableState
    {
        void Enter();      
    }

}
