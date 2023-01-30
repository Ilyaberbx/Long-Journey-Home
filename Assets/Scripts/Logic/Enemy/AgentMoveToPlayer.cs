using UnityEngine;
using UnityEngine.AI;

namespace Logic.Enemy
{
    public class AgentMoveToPlayer : MonoBehaviour
    {
        private const float MinimalDistance = 18f;

        [SerializeField] private NavMeshAgent _agent;
        private Transform _playerTransform;

        public void Construct(Transform playerTransform)
            => _playerTransform = playerTransform;
        
        private void Update()
        {
            if (Initialized() && HeroNotTouched())
                _agent.destination = _playerTransform.position;
            else
                _agent.destination = _agent.transform.position;
        }

        private bool Initialized()
            => _playerTransform != null;

        private bool HeroNotTouched()
            => Vector3.Distance(_agent.transform.position, _playerTransform.position) >= MinimalDistance;
    }
}