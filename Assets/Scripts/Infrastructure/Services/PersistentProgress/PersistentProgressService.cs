using Data;
using Infrastructure.Interfaces;
using Infrastructure.Services.SaveLoad;

namespace Infrastructure.Services.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        private const string SceneByDefault = "IntroRoad";
        public PlayerProgress PlayerProgress { get; set; }
        
        public PlayerProgress DefaultProgress()
        {
            PlayerProgress progress = new PlayerProgress(SceneByDefault);

            progress.HealthState.MaxHP = 100;
            progress.HealthState.ResetHp();
            progress.FlashLightState.MaxLightIntensity = 1500;
            progress.FlashLightState.Reset();
            progress.FreezeState.MaxFreeze = 100;
            progress.FreezeState.ResetFreeze();
            progress.InventoryData.Init(30);

            return progress;
        }
    }
}