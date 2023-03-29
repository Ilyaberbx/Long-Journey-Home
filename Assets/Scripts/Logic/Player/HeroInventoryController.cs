using Data;
using Infrastructure.Interfaces;
using Infrastructure.Services.StaticData;
using Logic.Inventory;
using UnityEngine;

namespace Logic.Player
{
    public class HeroInventoryController : MonoBehaviour, ISavedProgressWriter, IInventoryController
    {
        private IStaticDataService _staticData;
        private InventoryData _inventoryData;

        public void Construct(IStaticDataService staticData)
            => _staticData = staticData;

        public void AddItem(ItemType type)
        {
            var itemData = _staticData.GetItemByType(type);
            _inventoryData.AddItem(itemData);
        }

        public void RemoveItem(ItemType type)
        {
            var itemData = _staticData.GetItemByType(type);
            _inventoryData.RemoveItem(itemData);
        }

        public void UseItem(ItemType type)
        {
            var itemData = _staticData.GetItemByType(type);
            var item = Instantiate(itemData.Prefab);
            item.Use(GetComponent<HeroMover>());
        }

        public void LoadProgress(PlayerProgress progress)
            => _inventoryData = progress.InventoryData;

        public void UpdateProgress(PlayerProgress progress)
            => progress.InventoryData = _inventoryData;
    }
}