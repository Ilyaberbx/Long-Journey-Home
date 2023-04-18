using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Services.SaveLoad;
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

        Task<GameObject> CreatePlayer(Vector3 at);
        Task<GameObject> CreateHud();
        void CleanUp();
        Task<GameObject> CreateEnemy(EnemyType enemyType, Transform transform);
        Task<EnemySpawnPoint> CreateEnemySpawner(Vector3 at, string spawnerId, EnemyType spawnerEnemyType);
        ItemPickUp CreateItemPickUp(ItemData data, Transform transform);
        Task<LootSpawnPoint> CreateLootSpawner(Vector3 position, string id, Quaternion rotation, ItemData data);
        Task WarmUp();
    }
}