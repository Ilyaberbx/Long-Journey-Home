using UnityEngine;
using UnityEngine.AI;

namespace Logic.Enemy
{
    public class AgentMoveToPlayer : MonoBehaviour
    {
        [SerializeField] private EnemyAttack _attack;
        [SerializeField] private float _minimalDistance;
        [SerializeField] private NavMeshAgent _agent;
        private Transform _playerTransform;

        public void Construct(Transform playerTransform)
            => _playerTransform = playerTransform;
        
        private void Update()
        {
            Debug.Log(CanChase());
            if (CanChase())
                _agent.destination = _playerTransform.position;
            else
                _agent.destination = _agent.transform.position;
        }

        private bool CanChase() 
            => Initialized() && HeroNotTouched() && _agent.isOnNavMesh && !_attack.IsAttacking;

        public void Stop()
        {
            _agent.destination = transform.position;
            _agent.isStopped = true;
        }
        
        private bool Initialized()
            => _playerTransform != null;
        

        private bool HeroNotTouched()
            => Vector3.Distance(_agent.transform.position, _playerTransform.position) >= _minimalDistance;
    }
}