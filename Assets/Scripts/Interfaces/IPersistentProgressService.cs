using Interfaces;
using ProjectSolitude.Data;

namespace ProjectSolitude.Infrastructure.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        public PlayerProgress PlayerProgress { get; set; }
    }
}