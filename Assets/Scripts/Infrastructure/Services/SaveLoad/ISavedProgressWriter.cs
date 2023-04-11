using Data;

namespace Infrastructure.Services.SaveLoad
{
    public interface ISavedProgressWriter : ISavedProgressReader
    {
        void UpdateProgress(PlayerProgress progress);

    }
}