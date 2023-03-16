using Data;

namespace Infrastructure.Interfaces
{
    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }
}