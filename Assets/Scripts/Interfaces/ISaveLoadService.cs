using Interfaces;
using ProjectSolitude.Data;

namespace ProjectSolitude.Interfaces
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}