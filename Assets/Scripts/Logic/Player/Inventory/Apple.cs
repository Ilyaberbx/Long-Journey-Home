using System;

namespace Logic.Player.Inventory
{
    public class Apple : IInventoryItem
    {
        public bool IsEquipped { get; set; }
        public Type Type => GetType();
        public int MaxItemsInInventorySlot { get; }
        public int Amount { get; set; }

        public Apple(int maxItemsInInventorySlot) 
            => MaxItemsInInventorySlot = maxItemsInInventorySlot;

        public IInventoryItem Clone()
        {
            var clone = new Apple(MaxItemsInInventorySlot);
            clone.Amount = Amount;
            return clone;
        }
    }
}