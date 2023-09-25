using UnityEngine;

namespace Infrastructure.Services.EventBus.Handlers
{

    public interface IPlayerSpawnHandler : IGlobalSubscriber
    {
        void HandlePlayerSpawn(Transform player);
    }
}