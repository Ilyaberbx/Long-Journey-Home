using System;

namespace Logic.Inventory.Item
{

    [Serializable]
    public struct InventoryItem
    {
        public int Quantity;
        public ItemData ItemData;

        public bool IsEmpty => ItemData == null;

        public InventoryItem(ItemData itemData, int quantity)
        {
            ItemData = itemData;
            Quantity = quantity;
        }

        public InventoryItem ChangeQuantity(int newQuantity)
        {
            var item = new InventoryItem();
            item.Quantity = newQuantity;
            item.ItemData = ItemData;
            return item;
        }

        public static InventoryItem GetEmptyItem()
            => new InventoryItem(null, 0);

    }
}