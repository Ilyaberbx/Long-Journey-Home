using Data;
using Interfaces;
using Logic;
using StaticData;

namespace Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();
        EnemyData GetEnemyDataByType(EnemyType type);
        LevelData GetLevelData(string sceneKey);
    }
}