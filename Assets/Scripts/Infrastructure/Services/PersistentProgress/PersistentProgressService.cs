using ProjectSolitude.Data;

namespace ProjectSolitude.Infrastructure.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgress PlayerProgress { get; set; }
    }
}