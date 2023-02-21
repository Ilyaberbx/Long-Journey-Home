using System.Collections.Generic;
using Logic;
using Logic.Spawners;
using UnityEngine;

namespace Interfaces
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgressWriter> ProgressWriters { get; }

        GameObject CreatePlayer(Vector3 at);
        GameObject CreateHud();
        void CleanUp();
        GameObject CreateEnemy(EnemyType enemyType, Transform transform);
        SpawnPoint CreateSpawner(Vector3 at, string spawnerId, EnemyType spawnerEnemyType);
    }
}