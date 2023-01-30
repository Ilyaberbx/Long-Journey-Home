using Data;
using Infrastructure.Services;
using Interfaces;
using Logic.Enemy;
using UnityEngine;
using UnityEngine.Serialization;

namespace Logic.Spawners
{
    public class EnemySpawner : MonoBehaviour, ISavedProgressWriter
    {
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private bool _isSlain;

        private string _id;
        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
            _factory = ServiceLocator.Container.Single<IGameFactory>();
        }

        private void Spawn()
        {
            GameObject enemy = _factory.CreateEnemy(_enemyType, transform);
           _enemyDeath = enemy.GetComponent<EnemyDeath>();
           _enemyDeath.OnDie += Slay;
        }

        private void Slay()
        {
            if (_enemyDeath != null)
                _enemyDeath.OnDie -= Slay;
            
            _isSlain = true;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(_id))
                _isSlain = true;
            else
                Spawn();
        }


        public void UpdateProgress(PlayerProgress progress)
        {
            if (_isSlain)
                progress.KillData.ClearedSpawners.Add(_id);
        }
    }
}