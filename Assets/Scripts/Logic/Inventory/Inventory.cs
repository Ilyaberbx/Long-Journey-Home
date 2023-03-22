using System.Collections.Generic;
using Logic.Inventory.Item;

namespace Logic.Inventory
{
    public class Inventory : IInventory
    {
        private List<IItem> _items;
        public Inventory()
        {
            _items = new List<IItem>();
            AddItem(new InventoryItem());
            AddItem(new InventoryItem());
            AddItem(new InventoryItem());
            AddItem(new InventoryItem());
            AddItem(new InventoryItem());
        }

        public void AddItem(IItem item) 
            => _items.Add(item);

        public List<IItem> GetItems() 
            => _items;
    }
}