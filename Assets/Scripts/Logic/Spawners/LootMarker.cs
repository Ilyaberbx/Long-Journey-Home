using Logic.Inventory.Item;
using UnityEngine;

namespace Logic.Spawners
{
    public class LootMarker : BaseMarker
    {
        [SerializeField] private ItemPickUp _itemPickUp;
        public ItemPickUp Prefab => _itemPickUp;
    }
}