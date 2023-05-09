using Data;
using Logic.Enemy;
using Logic.Inventory.Item;
using StaticData;
using UI.Services.Window;

namespace Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();
        EnemyData GetEnemyDataByType(EnemyType type);
        LevelData GetLevelData(string sceneKey);
        WindowConfig GetWindowData(WindowType windowType);

        ItemPickUp GetPickUpByData(ItemData data);
    }
}