namespace ProjectSolitude.Interfaces
{
    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload payLoad);
    }
}
