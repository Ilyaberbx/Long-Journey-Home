using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload payLoad);
    }
    
    public interface IPayloadedState<in TPayload, in TVPayload> : IExitableState
    {
        Task Enter(TPayload tPayLoad, TVPayload tvPayload);
    }
}
