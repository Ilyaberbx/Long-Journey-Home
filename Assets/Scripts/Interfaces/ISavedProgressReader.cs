using ProjectSolitude.Data;

namespace ProjectSolitude.Interfaces
{
    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }
}