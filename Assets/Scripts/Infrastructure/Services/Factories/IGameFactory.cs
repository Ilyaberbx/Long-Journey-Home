using System.Collections.Generic;
using Infrastructure.Services.SaveLoad;
using Logic;
using Logic.Enemy;
using Logic.Inventory.Item;
using Logic.Spawners;
using UnityEngine;

namespace Infrastructure.Services.Factories
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgressWriter> ProgressWriters { get; }

        GameObject CreatePlayer(Vector3 at);
        GameObject CreateHud();
        void CleanUp();
        GameObject CreateEnemy(EnemyType enemyType, Transform transform);
        EnemySpawnPoint CreateEnemySpawner(Vector3 at, string spawnerId, EnemyType spawnerEnemyType);
        ItemPickUp CreateItemPickUp(ItemData data, Transform transform);
        LootSpawnPoint CreateLootSpawner(Vector3 position, string id, Quaternion rotation, ItemData data);
    }
}