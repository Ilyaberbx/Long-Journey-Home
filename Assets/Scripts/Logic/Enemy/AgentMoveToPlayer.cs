using ProjectSolitude.Infrastructure;
using UnityEngine;
using UnityEngine.AI;

public class AgentMoveToPlayer : MonoBehaviour
{
    private const float MinimalDistance = 18f;

    [SerializeField] private NavMeshAgent _agent;
    private Transform _playerTransform;
    private IGameFactory _gameFactory;

    private void Start()
    {
        _gameFactory = ServiceLocator.Container.Single<IGameFactory>();
        
        if (_gameFactory.HeroGameObject != null)
            InitializeHeroTransform();
        else
            _gameFactory.OnHeroCreated += HeroCreated;
    }


    private void Update()
    {
        if (Initialized() && HeroNotTouched())
            _agent.destination = _playerTransform.position;
        else
            _agent.destination = _agent.transform.position;
    }

    private bool Initialized()
        => _playerTransform != null;

    private void HeroCreated()
        => InitializeHeroTransform();

    private void InitializeHeroTransform()
        => _playerTransform = _gameFactory.HeroGameObject.transform;

    private bool HeroNotTouched()
        => Vector3.Distance(_agent.transform.position, _playerTransform.position) >= MinimalDistance;
}