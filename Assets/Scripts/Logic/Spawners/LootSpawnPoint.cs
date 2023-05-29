using Data;
using Infrastructure.Services.Factories;
using Infrastructure.Services.SaveLoad;
using Logic.Inventory.Item;
using UnityEngine;
using Zenject;

namespace Logic.Spawners
{
    public class LootSpawnPoint : MonoBehaviour,ISavedProgressWriter
    {
        private string _id;
        private IGameFactory _factory;
        private ItemPickUp _prefab;
        private bool _isPickedUp;

        [Inject]
        public void Construct(IGameFactory gameFactory) 
            => _factory = gameFactory;
        
        public void SetId(string id) 
            => _id = id;

        public void SetItemPickUp(ItemPickUp prefab) 
            => _prefab = prefab;

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.PickUpLootData.PickedUpLoot.Contains(_id))
                _isPickedUp = true;
            else
                Spawn();
        }

        private void Spawn()
        {
            ItemPickUp pickUp = _factory.CreateItemPickUp(_prefab,transform);
            pickUp.OnPickedUp += () => _isPickedUp = true;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
          if(_isPickedUp)
              progress.PickUpLootData.PickedUpLoot.Add(_id);
        }
    }
}