using Data;

namespace Infrastructure.Interfaces
{
    public interface ISavedProgressWriter : ISavedProgressReader
    {
        void UpdateProgress(PlayerProgress progress);

    }
}