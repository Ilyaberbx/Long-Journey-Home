using Data;

namespace Infrastructure.Interfaces
{
    public interface IPersistentProgressService : IService
    {
        public PlayerProgress PlayerProgress { get; set; }
    }
}