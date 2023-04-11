using Logic.Inventory.Item;
using UnityEngine;

namespace Logic.Spawners
{
    public class LootMarker : BaseMarker
    {
        [SerializeField] private ItemData _itemData;
        public ItemData Data => _itemData;
    }
}