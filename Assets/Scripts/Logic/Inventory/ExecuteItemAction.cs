﻿using Data;
using UI.Inventory;
using UnityEngine;

namespace Logic.Inventory
{
    public class ExecuteItemAction : IActionListener
    {
        private readonly InventoryData _inventoryData;
        private readonly InventoryWindow _view;
        private readonly int _index;
        private readonly GameObject _sender;

        public ExecuteItemAction(InventoryData inventoryData,InventoryWindow view,int index,GameObject sender)
        {
            _inventoryData = inventoryData;
            _view = view;
            _index = index;
            _sender = sender;
        }

        public void ExecuteAction()
        {
            InventoryItem item = _inventoryData.GetItemByIndex(_index);

            if (item.IsEmpty)
                return;

            if (item.ItemData is IDestroyableItem)
                _inventoryData.RemoveItem(_index, 1);

            if (item.ItemData is IItemAction action)
            {
                action.PerformAction(_sender);
                if (_inventoryData.GetItemByIndex(_index).IsEmpty) 
                    _view.ResetSelection();
            }
        }
    }
}