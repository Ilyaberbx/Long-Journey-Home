using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload payLoad);
    }
}
