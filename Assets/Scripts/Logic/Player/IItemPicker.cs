using Logic.Inventory.Item;

namespace Logic.Player
{
    public interface IItemPicker
    {
        bool TryPickUpItem(ItemData itemData, int quantity, out int reminder);
    }
}