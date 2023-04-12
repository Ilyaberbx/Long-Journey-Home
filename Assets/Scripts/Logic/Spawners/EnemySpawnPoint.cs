using Data;
using Infrastructure.Interfaces;
using Infrastructure.Services.Factories;
using Infrastructure.Services.SaveLoad;
using Logic.Enemy;
using UnityEngine;
using Zenject;

namespace Logic.Spawners
{
    public class EnemySpawnPoint : MonoBehaviour, ISavedProgressWriter
    {
        [SerializeField] private bool _isSlain;

        private EnemyType _enemyType;
        private string _id;
        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;

        [Inject]
        public void Construct(IGameFactory gameFactory) 
            => _factory = gameFactory;

        public void SetId(string id) 
            => _id = id;

        public void SetType(EnemyType spawnerEnemyType) 
            => _enemyType = spawnerEnemyType;

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
    }
}