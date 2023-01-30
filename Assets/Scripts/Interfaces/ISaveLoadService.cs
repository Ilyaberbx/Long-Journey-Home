using Data;
using ProjectSolitude.Data;

namespace Interfaces
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}