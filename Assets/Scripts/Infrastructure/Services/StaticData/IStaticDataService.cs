using System.Threading.Tasks;
using Data;
using Logic.Enemy;
using Logic.Inventory.Item;
using UI.Services.Window;

namespace Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        Task Load();
        EnemyData GetEnemyDataByType(EnemyType type);
        LevelData GetLevelData(string sceneKey);
        WindowConfig GetWindowData(WindowType windowType);
        
    }
}