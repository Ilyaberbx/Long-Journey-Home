using Logic;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy", order = 0)]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private EnemyType _type;

        [Range(1, 10)] [SerializeField] private int _maxHp;
        [Range(0, 10)] [SerializeField] private int _damage;
        [Range(0, 10)] [SerializeField] private float _attackCoolDown;
        [Range(0, 100)] [SerializeField] private float _moveSpeed;
        
        [Range(0, 5)][SerializeField] private int _minLoot;
        [Range(0, 5)][SerializeField] private int _maxLoot;

        public int MaxHp => _maxHp;
        public int Damage => _damage;
        public float AttackCoolDown => _attackCoolDown;
        public float MoveSpeed => _moveSpeed;
        public int MinLoot => _minLoot;
        public int MaxLoot => _maxLoot;
        public GameObject Prefab => _prefab;
        public EnemyType Type => _type;
    }
}