using Logic.Enemy;
using UnityEngine;

namespace Logic.Spawners
{
    public class EnemyMarker : BaseMarker
    {
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private bool _isRegisterInContainer;

        public EnemyType EnemyType => _enemyType;
        public bool IsRegisterInContainer => _isRegisterInContainer;
    }
}