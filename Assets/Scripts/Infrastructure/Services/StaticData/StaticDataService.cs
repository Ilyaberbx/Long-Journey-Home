using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Data;
using Logic;
using StaticData;

namespace Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string EnemyStaticDataPath = "StaticData/Enemies";
        private const string LevelStaticDataPath = "StaticData/Levels";
        private Dictionary<EnemyType, EnemyData> _enemies;
        private Dictionary<string, LevelData> _levels;

        public void Load()
        {
            _enemies = LoadEnemisData();

            _levels = LoadLevelsData();
        }

        private Dictionary<string, LevelData> LoadLevelsData()
        {
            return Resources.
                LoadAll<LevelData>(LevelStaticDataPath)
                .ToDictionary(x => x.LevelKey, x => x);
        }

        private Dictionary<EnemyType, EnemyData> LoadEnemisData()
        {
            return Resources.
                LoadAll<EnemyData>(EnemyStaticDataPath)
                .ToDictionary(x => x.Type, x => x);
        }

        public EnemyData GetEnemyDataByType(EnemyType type)
        {
            return _enemies.TryGetValue(type, out EnemyData data) 
                ? data 
                : null;
        }

        public LevelData GetLevelData(string sceneKey)
        {
            return _levels.TryGetValue(sceneKey, out LevelData data) 
                ? data 
                : null;
        }
    }
}