namespace Logic.Player.Inventory
{
    public interface IInventoryItemState
    {
        int Amount { get; set; }
        bool IsEquipped { get; set; }
    }
}