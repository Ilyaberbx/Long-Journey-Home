using System.Collections.Generic;
using System.Threading.Tasks;
using Data;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.Pause;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.StaticData;
using Logic.Enemy;
using Logic.Inventory;
using Logic.Inventory.Item;
using Logic.Player;
using Logic.Spawners;
using Logic.Weapons;
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
        private readonly IPauseService _pauseService;
        private readonly IPersistentProgressService _progressService;
        private readonly DiContainer _container;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgressWriter> ProgressWriters { get; } = new List<ISavedProgressWriter>();

        private GameObject _heroGameObject;
        private GameObject _createdObjectsContainer;
        private PlayerUIActor _uiActor;


        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticData
            , IWindowService windowService, IPauseService pauseService, IPersistentProgressService progressService, DiContainer container)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
            _windowService = windowService;
            _pauseService = pauseService;
            _progressService = progressService;
            _container = container;
        }

        public async Task WarmUp()
        {
            await _assetProvider.Load<GameObject>(AssetsAddress.LootSpawnPoint);
            await _assetProvider.Load<GameObject>(AssetsAddress.EnemySpawnPoint);
            await _assetProvider.Load<GameObject>(AssetsAddress.PlayerPrefabPath);
            await _assetProvider.Load<GameObject>(AssetsAddress.HudPath);
            await _assetProvider.Load<GameObject>(AssetsAddress.UIRoot);
        }

        public void CreateContainerForCreatedObjects()
            => _createdObjectsContainer = new GameObject("Container");

        public BaseEquippableItem CreateEquippableItem(BaseEquippableItem itemPrefab, Vector3 at,
            Transform container)
        {
            GameObject equipItem = InstantiateRegistered(itemPrefab.gameObject, at);
            
            if(equipItem.TryGetComponent(out  ISavedProgressReader reader))
                reader.LoadProgress(_progressService.PlayerProgress);

            if (equipItem.TryGetComponent(out IHudAmmoShowable ammoShowable))
                _uiActor.RegisterAmmoShowableObject(ammoShowable);
            
            equipItem.transform.SetParent(container);

            return equipItem.GetComponent<BaseEquippableItem>();
        }

        public async Task<GameObject> CreatePlayer(Vector3 at)
        {
            GameObject playerPrefab = await _assetProvider.Load<GameObject>(AssetsAddress.PlayerPrefabPath);
            _heroGameObject = InstantiateRegistered(playerPrefab, at);

            InventoryPresenter inventory = _heroGameObject.GetComponent<InventoryPresenter>();
            _windowService.Init(inventory);
            return _heroGameObject;
        }

        public async Task<GameObject> CreateHud()
        {
            GameObject hudPrefab = await _assetProvider.Load<GameObject>(AssetsAddress.HudPath);
            GameObject hud = InstantiateRegistered(hudPrefab);
            _uiActor = hud.GetComponent<PlayerUIActor>();
            return hud;
        }


        public async Task<GameObject> CreateEnemy(EnemyType enemyType, Transform parent)
        {
            EnemyData enemyData = _staticData.GetEnemyDataByType(enemyType);
            GameObject enemyPrefab = await _assetProvider.Load<GameObject>(enemyData.PrefabReference);

            GameObject enemy = InstantiateRegistered(enemyPrefab, parent.position);
            enemy.transform.SetParent(parent);

            IHealth health = enemy.GetComponent<IHealth>();
            health.CurrentHealth = enemyData.MaxHp;
            health.MaxHp = enemyData.MaxHp;

            enemy.GetComponent<UIActor>()?.Construct(health);
            enemy.GetComponent<AgentMoveToPlayer>().Construct(_heroGameObject.transform);
            enemy.GetComponent<NavMeshAgent>().speed = enemyData.MoveSpeed;

            EnemyAttack attack = enemy.GetComponent<EnemyAttack>();
            attack.Construct(_heroGameObject.transform, enemyData.Damage, enemyData.AttackCoolDown);
            return enemy;
        }

        public async Task<EnemySpawnPoint> CreateEnemySpawner(Vector3 at, string spawnerId, EnemyType spawnerEnemyType)
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(AssetsAddress.EnemySpawnPoint);

            EnemySpawnPoint spawner = InstantiateRegistered(prefab, at)
                .GetComponent<EnemySpawnPoint>();

            spawner.SetId(spawnerId);
            spawner.SetType(spawnerEnemyType);
            return spawner;
        }
        

        public ItemPickUp CreateItemPickUp(ItemPickUp prefab, Transform parent)
        {
            ItemPickUp spawnedPickUp = Object.Instantiate(prefab, parent.position, parent.rotation, parent);
            return spawnedPickUp;
        }

        public async Task<LootSpawnPoint> CreateLootSpawner(Vector3 at, string id, Quaternion rotation, ItemPickUp data)
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(AssetsAddress.LootSpawnPoint);

            LootSpawnPoint spawner = InstantiateRegistered(prefab, at)
                .GetComponent<LootSpawnPoint>();

            spawner.SetId(id);
            spawner.SetItemPickUp(data);
            spawner.transform.rotation = rotation;
            return spawner;
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
            _assetProvider.CleanUp();
        }

        private GameObject InstantiateRegistered(GameObject prefab, Vector3 at)
        {
            GameObject obj =
                _container.InstantiatePrefab(prefab, at, Quaternion.identity, _createdObjectsContainer.transform);
            RegisterProgressWatchers(obj);
            RegisterPauseWatchers(obj);
            return obj;
        }

        private GameObject InstantiateRegistered(GameObject prefab)
        {
            GameObject obj = _container.InstantiatePrefab(prefab);
            obj.transform.SetParent(_createdObjectsContainer.transform);
            RegisterProgressWatchers(obj);
            RegisterPauseWatchers(obj);
            return obj;
        }
        
        private void RegisterPauseWatchers(GameObject obj)
        {
            foreach (IPauseHandler handler in obj.GetComponentsInChildren<IPauseHandler>())
                _pauseService.Register(handler);
        }

        private void RegisterProgressWatchers(GameObject obj)
        {
            foreach (ISavedProgressReader progressReader in obj.GetComponentsInChildren<ISavedProgressReader>())
                RegisterProgressWriters(progressReader);
        }

        private void RegisterProgressWriters(ISavedProgressReader obj)
        {
            if (obj is ISavedProgressWriter writer)
                ProgressWriters.Add(writer);
            
            ProgressReaders.Add(obj);
        }
    }
}