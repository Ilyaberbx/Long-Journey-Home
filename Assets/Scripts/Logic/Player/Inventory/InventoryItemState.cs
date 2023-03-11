using System;

namespace Logic.Player.Inventory
{
    [Serializable]
    public class InventoryItemState : IInventoryItemState
    {
        public int ItemAmount;
        public bool IsItemEquipped;
        public int Amount { get => ItemAmount; 
            set => ItemAmount = value; }
        public bool IsEquipped { get => IsItemEquipped;
            set => IsItemEquipped = value; }

        public InventoryItemState()
        {
            ItemAmount = 0;
            IsItemEquipped = false;
        }
    }
}