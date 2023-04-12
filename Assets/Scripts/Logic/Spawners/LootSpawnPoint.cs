using Data;
using Infrastructure.Interfaces;
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
        private ItemData _data;
        private bool _isPickedUp;

        [Inject]
        public void Construct(IGameFactory gameFactory) 
            => _factory = gameFactory;
        
        public void SetId(string id) 
            => _id = id;

        public void SetData(ItemData data) 
            => _data = data;

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.PickUpLootData.PickedUpLoot.Contains(_id))
                _isPickedUp = true;
            else
                Spawn();
        }

        private void Spawn()
        {
            ItemPickUp pickUp = _factory.CreateItemPickUp(_data,transform);
            pickUp.OnPickedUp += () => _isPickedUp = true;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
          if(_isPickedUp)
              progress.PickUpLootData.PickedUpLoot.Add(_id);
        }
    }
}