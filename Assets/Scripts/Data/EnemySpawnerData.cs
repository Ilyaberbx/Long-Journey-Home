using System;
using Logic.Enemy;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class EnemySpawnerData
    {
        public string Id;
        public EnemyType EnemyType;
        public Vector3 Position;
        public bool IsRegisterInContainer;

        public EnemySpawnerData(string id, EnemyType enemyType, Vector3 position,bool isRegisterInContainer)
        {
            Id = id;
            EnemyType = enemyType;
            Position = position;
            IsRegisterInContainer = isRegisterInContainer;
        }
    }
}