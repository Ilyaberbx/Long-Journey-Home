using Data;
using Infrastructure.Interfaces;
using Interfaces;
using Logic;
using StaticData;
using UI.Services.Window;
using UnityEditor.PackageManager.UI;

namespace Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();
        EnemyData GetEnemyDataByType(EnemyType type);
        LevelData GetLevelData(string sceneKey);
        WindowConfig GetWindowData(WindowType windowType);
    }
}