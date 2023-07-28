using System.Collections.Generic;
using Data;
using Infrastructure.Services.SaveLoad;
using Logic.Inventory.Actions;
using Logic.Inventory.Item;
using UI.Inventory;
using UnityEngine;

namespace Logic.Inventory
{
    public class InventoryPresenter : MonoBehaviour, ISavedProgressWriter
    {
        private const string Drop = "Drop";

        [SerializeField] private List<InventoryItem> _initialItems;
        private InventoryWindow _inventoryWindow;
        private InventoryData _inventoryData;

        public void InitUI(InventoryWindow window)
        {
            _inventoryWindow = window;
            PrepareUI();
        }

        public bool TryRemoveItemById(int id, int amount)
            => _inventoryData.TryRemoveItemById(id, amount);

        public bool HasItem(int id)
            => _inventoryData.HasItemById(id);

        private void AddInitialItems()
        {
            foreach (var item in _initialItems)
                _inventoryData.AddItem(item.ItemData, item.Quantity);
        }


        private void PrepareUI()
        {
            _inventoryWindow.InitializeInventoryUI(_inventoryData.GetSize());
            _inventoryWindow.OnDescriptionRequested += HandleDescriptionRequest;
            _inventoryWindow.OnItemActionRequested += HandleItemActionRequest;
        }


        private void HandleItemActionRequest(int index)
        {
            IAction itemAction;
            InventoryItem item = _inventoryData.GetItemByIndex(index);

            if (item.IsEmpty)
                return;

            if (item.ItemData is IItemAction action)
            {
                itemAction = new ExecuteItemAction(_inventoryData, _inventoryWindow, index, gameObject);
                _inventoryWindow.ShowActionPanelByIndex(index);
                _inventoryWindow.AddAction(action.ActionName, itemAction);
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

        public void LoadProgress(PlayerProgress progress)
        {
            _inventoryData = progress.InventoryData;

            if (_inventoryData.IsEmpty())
                AddInitialItems();
        }

        public void UpdateProgress(PlayerProgress progress)
            => progress.InventoryData = _inventoryData;
    }
}