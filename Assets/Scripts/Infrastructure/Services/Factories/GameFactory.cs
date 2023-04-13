using System.Collections.Generic;
using Data;
using Infrastructure.Interfaces;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.Input;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.StaticData;
using Logic;
using Logic.Enemy;
using Logic.Inventory;
using Logic.Inventory.Item;
using Logic.Player;
using Logic.Spawners;
using StaticData;
using UI.Elements;
using UI.Services.Window;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Object = UnityEngine.Object;

namespace Infrastructure.Services.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;
        private readonly IWindowService _windowService;
        private readonly DiContainer _container;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgressWriter> ProgressWriters { get; } = new List<ISavedProgressWriter>();

        private GameObject _heroGameObject;


        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticData
            , IWindowService windowService,DiContainer container)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
            _windowService = windowService;
            _container = container;
        }

        public GameObject CreatePlayer(Vector3 at)
        {
            _heroGameObject = InstantiateRegistered(AssetsPath.PlayerPrefabPath, at);
            var inventory = _heroGameObject.GetComponent<InventoryAdapter>();
            _windowService.Init(inventory);
            return _heroGameObject;
        }

        public GameObject CreateHud()
            => InstantiateRegistered(AssetsPath.HudPath);


        public GameObject CreateEnemy(EnemyType enemyType, Transform parent)
        {
            EnemyData enemyData = _staticData.GetEnemyDataByType(enemyType);
            GameObject enemy = _container.InstantiatePrefab(enemyData.Prefab, parent.position, Quaternion.identity, parent);
            var health = enemy.GetComponent<IHealth>();
            health.CurrentHealth = enemyData.MaxHp;
            health.MaxHp = enemyData.MaxHp;

            enemy.GetComponent<UIActor>()?.Construct(health);
            enemy.GetComponent<AgentMoveToPlayer>().Construct(_heroGameObject.transform);
            enemy.GetComponent<NavMeshAgent>().speed = enemyData.MoveSpeed;

            EnemyAttack attack = enemy.GetComponent<EnemyAttack>();
            attack.Construct(_heroGameObject.transform, enemyData.Damage, enemyData.AttackCoolDown);
            return enemy;
        }

        public EnemySpawnPoint CreateEnemySpawner(Vector3 at, string spawnerId, EnemyType spawnerEnemyType)
        {
            EnemySpawnPoint spawner = InstantiateRegistered(AssetsPath.EnemySpawner, at).GetComponent<EnemySpawnPoint>();
            spawner.SetId(spawnerId);
            spawner.SetType(spawnerEnemyType); 
            return spawner;
        }

        public ItemPickUp CreateItemPickUp(ItemData data, Transform parent)
        {
            ItemPickUp pickUpPrefab = _staticData.GetPickUpByData(data);
            ItemPickUp spawnedPickUp = Object.Instantiate(pickUpPrefab, parent.position, parent.rotation, parent);
            return spawnedPickUp;
        }

        public LootSpawnPoint CreateLootSpawner(Vector3 at, string id, Quaternion rotation, ItemData data)
        {
            LootSpawnPoint spawner = InstantiateRegistered(AssetsPath.LootSpawner, at).GetComponent<LootSpawnPoint>();
            spawner.SetId(id);
            spawner.SetData(data);
            spawner.transform.rotation = rotation;
            return spawner;
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private GameObject InstantiateRegistered(string path, Vector3 at)
        {
            GameObject obj = _assetProvider.Instantiate(path, at);
            RegisterProgressWatchers(obj);
            return obj;
        }

        private GameObject InstantiateRegistered(string path)
        {
            GameObject obj = _assetProvider.Instantiate(path);
            RegisterProgressWatchers(obj);
            return obj;
        }

        private void RegisterProgressWatchers(GameObject obj)
        {
            foreach (ISavedProgressReader progressReader in obj.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        private void Register(ISavedProgressReader obj)
        {
            if (obj is ISavedProgressWriter writer)
                ProgressWriters.Add(writer);

            ProgressReaders.Add(obj);
        }
    }
}