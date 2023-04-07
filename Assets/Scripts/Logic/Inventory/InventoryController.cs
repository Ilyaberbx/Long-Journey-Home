using System.Collections.Generic;
using UI.Inventory;
using UnityEngine;

namespace Logic.Inventory
{
    public class InventoryController : MonoBehaviour
    {
        private const string Drop = "Drop";
        [SerializeField] private List<InventoryItem> _initialItems;
        [SerializeField] private InventoryData _inventoryData;
        [SerializeField] private InventoryWindow _inventoryWindow;

        private void Awake()
        {
            InitData();
            PrepareUI();
            AddInitialItems();
        }

        private void InitData()
        {
            _inventoryData.Init();
            _inventoryData.OnStateChanged += UpdateInventoryUI;
        }

        private void AddInitialItems()
        {
            foreach (var item in _initialItems)
                _inventoryData.AddItem(item.ItemData, item.Quantity);
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> itemsByIndex)
        {
            _inventoryWindow.ResetAllItems();
            
            foreach (var item in itemsByIndex)
                _inventoryWindow.UpdateData(item.Key
                    , item.Value.ItemData.Icon
                    , item.Value.Quantity);
        }


        private void PrepareUI()
        {
            _inventoryWindow.InitializeInventoryUI(_inventoryData.Size);
            _inventoryWindow.OnDescriptionRequested += HandleDescriptionRequest;
            _inventoryWindow.OnItemActionRequested += HandleItemActionRequest;
        }


        private void HandleItemActionRequest(int index)
        {
            IActionListener listener;
            InventoryItem item = _inventoryData.GetItemByIndex(index);

            if (item.IsEmpty)
                return;


            if (item.ItemData is IItemAction action)
            {
                listener = new ExecuteItemAction(_inventoryData, _inventoryWindow, index, gameObject);
                _inventoryWindow.ShowActionPanelByIndex(index);
                _inventoryWindow.AddAction(action.ActionName, listener);
            }

            if (item.ItemData is IDestroyableItem)
            {
                listener = new DropItemAction(_inventoryData, _inventoryWindow, index, item.Quantity);
                _inventoryWindow.AddAction(Drop, listener);
            }
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