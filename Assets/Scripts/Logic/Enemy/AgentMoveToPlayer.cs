using Infrastructure.Services.Pause;
using UnityEngine;
using UnityEngine.AI;

namespace Logic.Enemy
{
    public class AgentMoveToPlayer : MonoBehaviour, IPauseHandler
    {
        [SerializeField] private float _minimalDistance;
        [SerializeField] private NavMeshAgent _agent;
        private Transform _playerTransform;

        public void Init(Transform playerTransform)
            => _playerTransform = playerTransform;
        
        private void Update()
        {
            if (CanChase())
                _agent.destination = _playerTransform.position;
            else
                _agent.destination = _agent.transform.position;
        }

        private bool CanChase()
            => Initialized() && HeroNotTouched() && !_agent.isStopped && _agent.isOnNavMesh;
        public void Stop()
        {
            _agent.isStopped = true;
            _agent.destination = transform.position;
            _agent.velocity = Vector3.zero;
        }

        public void Resume()
            => _agent.isStopped = false;

        private bool Initialized()
            => _playerTransform != null;


        private bool HeroNotTouched()
            => Vector3.Distance(_agent.transform.position, _playerTransform.position) >= _minimalDistance;

        public void HandlePause(bool isPaused)
        {
            if (isPaused)
                Stop();
            else
                Resume();
        }
    }
}