using Data;
using DG.Tweening;
using Interfaces;
using UnityEngine;

namespace Logic.Enemy
{
    public class EnemyLootSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyDeath _enemyDeath;
        private IGameFactory _factory;
        private int _lootMax;
        private int _lootMin;

        public void Construct(IGameFactory factory)
        {
            _factory = factory;
            _enemyDeath.OnDisappear += Spawn;
        }

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }
        

        private void Spawn()
        {
            LootPiece loot = _factory.CreateLoot();
            AppearLoot(loot.gameObject);
            
            var lootItem = GenerateLootItem();
            
            loot.Initialize(lootItem);
        }

        private Loot GenerateLootItem()
        {
            return new Loot()
            {
                Value = Random.Range(_lootMin, _lootMax)
            };
        }

        private void AppearLoot(GameObject loot)
        {
            loot.transform.position = transform.position;
            Vector3 prevScale = loot.transform.localScale;
            loot.transform.localScale = Vector3.zero;
            loot.transform.DOScale(prevScale, 1f);
        }
    }
}