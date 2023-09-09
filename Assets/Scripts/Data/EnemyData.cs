using Logic.Enemy;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Data
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy", order = 0)]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private AssetReferenceGameObject _prefabReference;
        [SerializeField] private EnemyType _type;

        [Range(1, 100)] [SerializeField] private int _maxHp;
        [Range(0, 50)] [SerializeField] private int _damage;
        [Range(0, 10)] [SerializeField] private float _attackCoolDown;
        [Range(0, 200)] [SerializeField] private float _moveSpeed;

        public int MaxHp => _maxHp;
        public int Damage => _damage;
        public float AttackCoolDown => _attackCoolDown;
        public float MoveSpeed => _moveSpeed;
        public AssetReferenceGameObject PrefabReference => _prefabReference;
        public EnemyType Type => _type;
    }
}