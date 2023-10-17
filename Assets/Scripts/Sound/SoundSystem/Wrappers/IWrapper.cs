using System.Threading.Tasks;
using Infrastructure.Services.AssetManagement;

namespace Sound.SoundSystem.Wrappers
{

    public interface IWrapper
    {
        Task Initialize(IAssetProvider assets);
    }
}