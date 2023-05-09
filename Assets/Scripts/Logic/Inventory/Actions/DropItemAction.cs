using Data;
using Logic.Inventory.Item;
using UI.Inventory;

namespace Logic.Inventory.Actions
{
    public class DropItemAction : IAction
    {
        private readonly InventoryData _inventoryData;
        private readonly InventoryWindow _view;
        private readonly int _index;
        private readonly int _quantity;

        public DropItemAction(InventoryData inventoryData, InventoryWindow view,  int index,
            int quantity)
        {
            _inventoryData = inventoryData;
            _view = view;
            _index = index;
            _quantity = quantity;
        }

        public void ExecuteAction()
        {
            _inventoryData.RemoveItemByIndex(_index, _quantity);
            _view.ResetSelection();
        }
    }
}