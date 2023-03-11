using System;

namespace Logic.Player.Inventory
{
    public interface IInventoryItem
    {
        IInventoryItemInfo Info { get; }
        
        IInventoryItemState State { get; }
        Type Type { get; }

        IInventoryItem Clone();
    }
}