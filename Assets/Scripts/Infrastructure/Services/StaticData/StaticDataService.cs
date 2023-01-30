using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Logic;
using StaticData;

namespace Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string StaticDataPath = "StaticData/Enemies";
        private Dictionary<EnemyType, EnemyData> _enemies;

        public void LoadEnemies()
        {
            _enemies = Resources.LoadAll<EnemyData>(StaticDataPath).
                ToDictionary(x => x.Type, x => x);
        }

        public EnemyData GetEnemyDataByType(EnemyType type)
        {
            return _enemies.TryGetValue(type, out EnemyData data) 
                ? data 
                : null;
        }
    }
}