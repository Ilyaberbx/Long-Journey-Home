using Infrastructure.Services.EventBus;
using Infrastructure.Services.EventBus.Handlers;
using UnityEngine;
using Zenject;

namespace Logic.Common
{
    public class Snowfall : MonoBehaviour, IPlayerSpawnHandler
    {
        private IEventBusService _eventBus;
        private Transform _player;
        
        [Inject]
        public void Construct(IEventBusService eventBus)
            => _eventBus = eventBus;

        private void Awake()
            => _eventBus.Subscribe(this);

        private void OnDestroy()
            => _eventBus.Unsubscribe(this);

        public void HandlePlayerSpawn(Transform player)
            => _player = player;

        private void Update()
        {
            if (_player == null)
                return;
            
            transform.position = Vector3.Lerp(transform.position, _player.position, Time.deltaTime);
        }
    }
}