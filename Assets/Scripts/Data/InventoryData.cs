using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class InventoryData
    {
        public List<ItemData> Items;
        public Action OnStateChanged;

        public void AddItem(ItemData item)
        {
            Items.Add(item);
            OnStateChanged?.Invoke();
        }
    }
}