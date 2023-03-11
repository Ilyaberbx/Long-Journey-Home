using System;

namespace Logic.Player.Inventory
{
    public class Apple : IInventoryItem
    {
        public IInventoryItemInfo Info { get; }
        public IInventoryItemState State { get; }
        public Type Type => GetType();
        public Apple(IInventoryItemInfo info)
        {
            Info = info;
            State = new InventoryItemState();
        }

        public IInventoryItem Clone()
        {
            var clone = new Apple(Info)
            {
                State =
                {
                    Amount = State.Amount
                }
            };
            return clone;
        }
    }
}