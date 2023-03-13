using System;

namespace Logic.Player.Inventory
{
    public class Pepper : IInventoryItem
    {
        public IInventoryItemInfo Info { get; }
        public IInventoryItemState State { get; }
        public Type Type => GetType();
        public Pepper(IInventoryItemInfo info)
        {
            Info = info;
            State = new InventoryItemState();
        }

        public IInventoryItem Clone()
        {
            var clone = new Pepper(Info)
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