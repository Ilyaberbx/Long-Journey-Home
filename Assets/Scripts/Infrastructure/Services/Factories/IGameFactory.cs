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
        Task<GameObject> CreateDialogueView();
        void CleanUp();
        Task<GameObject> CreateEnemy(EnemyType enemyType, Transform transform, bool isRegisterInContainer = false);

        Task<EnemySpawnPoint> CreateEnemySpawner(Vector3 at, string spawnerId, EnemyType spawnerEnemyType,
            bool isRegisterInContainer);

        ItemPickUp CreateItemPickUp(ItemPickUp prefab, Transform transform);
        Task<LootSpawnPoint> CreateLootSpawner(Vector3 position, string id, Quaternion rotation, ItemPickUp prefab);
        Task WarmUp();
        void CreateContainerForCreatedObjects();

        BaseEquippableItem CreateEquippableItem(BaseEquippableItem itemPrefab, Vector3 equipmentPointPosition,
            Transform container);
    }
}