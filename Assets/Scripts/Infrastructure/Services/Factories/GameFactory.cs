using System.Collections.Generic;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.StaticData;
using Interfaces;
using Logic;
using Logic.Enemy;
using StaticData;
using UI;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace Infrastructure.Services.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;
        private readonly IPersistentProgressService _progressService;
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgressWriter> ProgressWriters { get; } = new List<ISavedProgressWriter>();

        private GameObject _heroGameObject;

        

        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticData,IPersistentProgressService progressService)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
            _progressService = progressService;
        }

        public GameObject CreatePlayer(Vector3 at)
        {
            _heroGameObject = InstantiateRegistered(AssetsPath.PlayerPrefabPath, at);
            return _heroGameObject;
        }

        public GameObject CreateHud()
            => InstantiateRegistered(AssetsPath.HudPath);

        public LootPiece CreateLoot()
        {
            var lootPiece = InstantiateRegistered(AssetsPath.Loot).GetComponent<LootPiece>();
            lootPiece.Construct(_progressService.PlayerProgress.FlashLightState);
            return lootPiece;
        }

        public GameObject CreateEnemy(EnemyType enemyType, Transform parent)
        {
            EnemyData enemyData = _staticData.GetEnemyDataByType(enemyType);
            GameObject enemy = Object.Instantiate(enemyData.Prefab, parent.position, Quaternion.identity, parent);
            var health = enemy.GetComponent<IHealth>();
            health.CurrentHealth = enemyData.MaxHp;
            health.MaxHp = enemyData.MaxHp;
            
            enemy.GetComponent<UIActor>()?.Construct(health);
            enemy.GetComponent<AgentMoveToPlayer>().Construct(_heroGameObject.transform);
            enemy.GetComponent<NavMeshAgent>().speed = enemyData.MoveSpeed;
            
            var loot = enemy.GetComponentInChildren<EnemyLootSpawner>();
            loot.SetLoot(enemyData.MinLoot,enemyData.MaxLoot);
            loot.Construct(this);
            

            EnemyAttack attack = enemy.GetComponent<EnemyAttack>();
            attack.Construct(_heroGameObject.transform,enemyData.Damage,enemyData.AttackCoolDown);
            return enemy;
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

        public void Register(ISavedProgressReader obj)
        {
            if (obj is ISavedProgressWriter writer)
                ProgressWriters.Add(writer);

            ProgressReaders.Add(obj);
        }
    }
}