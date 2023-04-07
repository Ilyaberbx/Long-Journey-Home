namespace Logic.Inventory
{
    public interface IHeroItemPicker
    {
        bool TryPickUpItem(ItemData itemData, int quantity, out int reminder);
    }
}