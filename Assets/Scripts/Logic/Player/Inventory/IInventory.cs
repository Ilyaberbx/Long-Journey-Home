using System;

namespace Logic.Player.Inventory
{
    public interface IInventory
    {
        int Capacity { get; }
        bool IsFull { get; }

        IInventoryItem GetItem(Type itemType);
        IInventoryItem[] GetAllItems();
        IInventoryItem[] GetEquippedItems();
        IInventoryItem[] GetAllItems(Type type);
        

        int GetItemAmount(Type type);
        bool TryAdd(IInventoryItem item);
        void Remove(Type type, int amount = 1);
        bool HasItem(Type type, out IInventoryItem item);
    }
}