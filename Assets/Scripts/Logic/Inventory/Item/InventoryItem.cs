using UnityEngine;

namespace Logic.Inventory.Item
{
    public class InventoryItem : IItem
    {
        public ItemType Type => _type;
        
        [SerializeField] private ItemType _type;
        [SerializeField] private int _amount;
    }
}