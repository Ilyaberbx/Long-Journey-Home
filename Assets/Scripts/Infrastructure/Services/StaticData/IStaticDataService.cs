using Interfaces;
using Logic;
using StaticData;

namespace Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadEnemies();
        EnemyData GetEnemyDataByType(EnemyType type);
    }
}