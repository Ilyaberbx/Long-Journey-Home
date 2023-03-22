using System.Collections.Generic;
using Logic.Inventory.Item;

namespace Logic.Inventory
{
    public interface IInventory
    {
        void AddItem(IItem item);

        public List<IItem> GetItems();
    }
}