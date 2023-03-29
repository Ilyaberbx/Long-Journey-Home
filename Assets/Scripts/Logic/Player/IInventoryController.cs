using Logic.Inventory;

namespace Logic.Player
{
    public interface IInventoryController
    {
        void AddItem(ItemType type);
        void RemoveItem(ItemType type);
        void UseItem(ItemType type);
    }
}