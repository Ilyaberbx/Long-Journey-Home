using System;

namespace Logic.Inventory
{

    [Serializable]
    public struct InventoryItem
    {
        public int Quantity;
        public ItemData ItemData;

        public InventoryItem(ItemData itemData, int quantity)
        {
            ItemData = itemData;
            Quantity = quantity;
        }

        public bool IsEmpty => ItemData == null;

        public InventoryItem ChangeQuantity(int newQuantity)
        {
            return new InventoryItem
            {
                ItemData = this.ItemData,
                Quantity = newQuantity,
            };
        }

        public static InventoryItem GetEmptyItem()
            => new InventoryItem(itemData: null, quantity: 0);

    }
}