using Logic;
using Logic.Enemy;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace StaticData
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy", order = 0)]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private AssetReferenceGameObject _prefabReference;
        [SerializeField] private EnemyType _type;

        [Range(1, 10)] [SerializeField] private int _maxHp;
        [Range(0, 10)] [SerializeField] private int _damage;
        [Range(0, 10)] [SerializeField] private float _attackCoolDown;
        [Range(0, 100)] [SerializeField] private float _moveSpeed;

        public int MaxHp => _maxHp;
        public int Damage => _damage;
        public float AttackCoolDown => _attackCoolDown;
        public float MoveSpeed => _moveSpeed;
        public AssetReferenceGameObject PrefabReference => _prefabReference;
        public EnemyType Type => _type;
    }
}