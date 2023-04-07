﻿using System.Collections.Generic;
using Data;
using Infrastructure.Interfaces;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.StaticData;
using Logic;
using Logic.Enemy;
using Logic.Inventory;
using Logic.Player;
using Logic.Spawners;
using StaticData;
using UI.Elements;
using UI.Services.Window;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace Infrastructure.Services.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;
        private readonly IWindowService _windowService;
        private readonly IInputService _inputService;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgressWriter> ProgressWriters { get; } = new List<ISavedProgressWriter>();

        private GameObject _heroGameObject;


        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticData
            , IWindowService windowService, IInputService inputService)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
            _windowService = windowService;
            _inputService = inputService;
        }

        public GameObject CreatePlayer(Vector3 at)
        {
            _heroGameObject = InstantiateRegistered(AssetsPath.PlayerPrefabPath, at);
            _heroGameObject.GetComponent<HeroWindowOpener>().Construct(_inputService, _windowService);
            _heroGameObject.GetComponent<HeroAttack>().Construct(_inputService);
            _heroGameObject.GetComponent<HeroInteractor>().Construct(_inputService);
            _heroGameObject.GetComponent<HeroLook>().Construct(_inputService);
            _heroGameObject.GetComponent<HeroMover>().Construct(_inputService);
            _heroGameObject.GetComponent<HeroEquipSwitcher>().Construct(_inputService);
            var inventory = _heroGameObject.GetComponent<InventoryViewHandler>();
            _windowService.Init(inventory);
            
            return _heroGameObject;
        }

        public GameObject CreateHud()
            => InstantiateRegistered(AssetsPath.HudPath);


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

            EnemyAttack attack = enemy.GetComponent<EnemyAttack>();
            attack.Construct(_heroGameObject.transform, enemyData.Damage, enemyData.AttackCoolDown);
            return enemy;
        }

        public SpawnPoint CreateSpawner(Vector3 at, string spawnerId, EnemyType spawnerEnemyType)
        {
            SpawnPoint spawner = InstantiateRegistered(AssetsPath.Spawner, at).GetComponent<SpawnPoint>();
            spawner.SetId(spawnerId);
            spawner.SetType(spawnerEnemyType);
            spawner.Construct(this);
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