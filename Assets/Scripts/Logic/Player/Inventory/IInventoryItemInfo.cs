using UnityEngine;

namespace Logic.Player.Inventory
{
    public interface IInventoryItemInfo
    {
        Sprite Icon { get; }
        string Id { get; }
        string Title { get; }
        string Description { get; }
        int MaxItemsInInventorySlot { get; }
    }
}