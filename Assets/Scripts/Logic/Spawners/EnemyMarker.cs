using Logic.Enemy;
using UnityEngine;

namespace Logic.Spawners
{
    public class EnemyMarker : BaseMarker
    {
        [SerializeField] private EnemyType _enemyType;

        public EnemyType EnemyType => _enemyType;
    }
}