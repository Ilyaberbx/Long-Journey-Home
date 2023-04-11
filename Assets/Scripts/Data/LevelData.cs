using System.Collections.Generic;
using Logic.Spawners;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "LevelData",menuName = "StaticData/Level")]
    public class LevelData : ScriptableObject
    {
        public string LevelKey;
        public List<EnemySpawnerData> EnemySpawners;
        public List<LootSpawnerData> LootSpawners;
        public Vector3 PlayerInitPoint;
    }
}