using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class InventoryData
    {
        public event Action OnInventoryStateChanged;
        private List<ItemData> _items;

        public InventoryData()
            => _items = new List<ItemData>();

        private void Debug()
        {
            foreach (var item in _items)
                UnityEngine.Debug.Log(item.Name);
        }

        public void AddItem(ItemData itemData)
        {
            _items.Add(itemData);
            OnInventoryStateChanged?.Invoke();
            Debug();
        }

        public void RemoveItem(ItemData itemData)
        {
            if (!_items.Contains(itemData)) return;

            _items.Remove(itemData);

            OnInventoryStateChanged?.Invoke();

            Debug();
        }
    }
}