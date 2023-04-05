using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Data;
using Logic;
using StaticData;
using UI.Services.Window;

namespace Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string EnemyStaticDataPath = "StaticData/Enemies";
        private const string LevelStaticDataPath = "StaticData/Levels";
        private const string WindowsStaticDataPath = "StaticData/UI/WindowsData";
        private Dictionary<EnemyType, EnemyData> _enemies;
        private Dictionary<string, LevelData> _levels;
        private Dictionary<WindowType, WindowConfig> _windows;

        public void Load()
        {
            _enemies = LoadEnemiesData();
            _levels = LoadLevelsData();
            _windows = LoadWindowData();
        }

        private Dictionary<WindowType, WindowConfig> LoadWindowData() 
            => Resources.Load<WindowsStaticData>(WindowsStaticDataPath)
                .Configs.ToDictionary(x => x.Type, x => x);
        

        private Dictionary<string, LevelData> LoadLevelsData() 
            => Resources.
                LoadAll<LevelData>(LevelStaticDataPath)
                .ToDictionary(x => x.LevelKey, x => x);

        private Dictionary<EnemyType, EnemyData> LoadEnemiesData() =>
            Resources.
                LoadAll<EnemyData>(EnemyStaticDataPath)
                .ToDictionary(x => x.Type, x => x);
        
        public EnemyData GetEnemyDataByType(EnemyType type) 
            => _enemies.TryGetValue(type, out EnemyData data) 
                ? data 
                : null;
        

        public LevelData GetLevelData(string sceneKey) 
            => _levels.TryGetValue(sceneKey, out LevelData data) 
                ? data 
                : null;

        public WindowConfig GetWindowData(WindowType windowType) 
            => _windows.TryGetValue(windowType, out WindowConfig config) 
                ? config 
                : null;
        
    }
}