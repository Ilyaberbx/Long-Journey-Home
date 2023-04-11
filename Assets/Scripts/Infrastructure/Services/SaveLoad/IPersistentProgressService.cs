using Data;

namespace Infrastructure.Services.SaveLoad
{
    public interface IPersistentProgressService : IService
    {
        public PlayerProgress PlayerProgress { get; set; }
    }
}