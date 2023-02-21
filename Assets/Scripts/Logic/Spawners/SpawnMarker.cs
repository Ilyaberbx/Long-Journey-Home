using UnityEngine;

namespace Logic.Spawners
{
    public class SpawnMarker : MonoBehaviour
    {
        [SerializeField] private EnemyType _enemyType;

        public EnemyType EnemyType => _enemyType;
    }
}