using Data;
using UI.Inventory;

namespace Logic.Inventory
{
    public class DropItemAction : IActionListener
    {
        private readonly InventoryData _inventoryData;
        private readonly InventoryWindow _view;
        private readonly int _index;
        private readonly int _quantity;

        public DropItemAction(InventoryData inventoryData,InventoryWindow view,int index,int quantity)
        {
            _inventoryData = inventoryData;
            _view = view;
            _index = index;
            _quantity = quantity;
        }
        public void ExecuteAction()
        {
            _inventoryData.RemoveItem(_index, _quantity);
            _view.ResetSelection();
        }
    }
}