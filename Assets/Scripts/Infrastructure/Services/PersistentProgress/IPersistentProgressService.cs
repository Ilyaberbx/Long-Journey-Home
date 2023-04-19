using Data;

namespace Infrastructure.Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        public PlayerProgress PlayerProgress { get; set; }

        public void ClearUp();
        PlayerProgress DefaultProgress();
    }
}