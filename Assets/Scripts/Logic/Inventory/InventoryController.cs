using UI.Inventory;
using UnityEngine;

namespace Logic.Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private InventoryData _inventoryData;
        [SerializeField] private InventoryWindow _inventoryWindow;

        private void Awake()
        {
            PrepareUI();
            //_inventoryData.Init();
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.A)) return;

            foreach (var item in _inventoryData.GetCurrentInventoryState())
            {
                _inventoryWindow.UpdateData(item.Key
                    , item.Value.ItemData.Icon
                    , item.Value.Quantity);
            }
        }

        private void PrepareUI()
        {
            _inventoryWindow.InitializeInventoryUI(_inventoryData.Size);
            _inventoryWindow.OnDescriptionRequested += HandleDescriptionRequest;
            _inventoryWindow.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleItemActionRequest(int index)
        {
        }

        private void HandleDescriptionRequest(int index)
        {
            InventoryItem item = _inventoryData.GetItemByIndex(index);

            if (item.IsEmpty)
            {
                _inventoryWindow.ResetSelection();
                return;
            }
               

            _inventoryWindow.UpdateDescription(index
                , item.ItemData.Icon, 
                item.ItemData.Name,
                item.ItemData.Description);
        }
    }
}