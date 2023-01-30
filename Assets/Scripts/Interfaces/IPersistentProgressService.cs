using Data;
using ProjectSolitude.Data;

namespace Interfaces
{
    public interface IPersistentProgressService : IService
    {
        public PlayerProgress PlayerProgress { get; set; }
    }
}