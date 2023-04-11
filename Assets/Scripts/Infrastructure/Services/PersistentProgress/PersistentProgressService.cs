using Data;
using Infrastructure.Interfaces;
using Infrastructure.Services.SaveLoad;

namespace Infrastructure.Services.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgress PlayerProgress { get; set; }
    }
}