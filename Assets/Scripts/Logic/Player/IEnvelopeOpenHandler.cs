using System.Threading.Tasks;
using Data;

namespace Logic.Player
{
    public interface IEnvelopeOpenHandler
    {
        Task OpenEnvelopeWindow(EnvelopeData data);
    }
}