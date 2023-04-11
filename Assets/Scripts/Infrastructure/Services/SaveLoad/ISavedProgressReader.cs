using Data;

namespace Infrastructure.Services.SaveLoad
{
    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }
}