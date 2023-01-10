
using ProjectSolitude.Data;

namespace ProjectSolitude.Interfaces
{
    public interface ISavedProgressWriter : ISavedProgressReader
    {
        void UpdateProgress(PlayerProgress progress);
    }
}