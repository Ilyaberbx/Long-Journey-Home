using System;
using UnityEngine;

namespace Logic.Player.Inventory
{
    public class InventorySlot : IInventorySlot
    {
        public bool IsFull => !IsEmpty && Amount == Capacity;
        public bool IsEmpty => Item == null;
        public int Amount => IsEmpty ? 0 : Item.State.Amount;
        public int Capacity { get; private set; }

        public IInventoryItem Item { get; private set; }
        public Type ItemType => Item.Type;

        public void SetItem(IInventoryItem item)
        {
            if (!IsEmpty)
                return;

            
            Debug.Log("SetSlot");
            
            Item = item;
            Capacity = item.Info.MaxItemsInInventorySlot;
        }

        public void Clear()
        {
            if(IsEmpty)
                return;

            Item.State.Amount = 0;
            Item = null;
        }
    }
}