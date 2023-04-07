﻿using System;
using System.Collections.Generic;
using Logic.Inventory;
using UI.Elements;
using UnityEngine;

namespace UI.Inventory
{
    public class InventoryWindow : WindowBase
    {
        public event Action<int> OnDescriptionRequested, OnItemActionRequested;

        [SerializeField] private ItemActionPanel _actionPanel;
        [SerializeField] private InventoryDescription _description;
        [SerializeField] private ItemView _itemPrefab;
        [SerializeField] private RectTransform _contentPanel;

        private List<ItemView> _itemViews = new List<ItemView>();

        protected override void Initialize()
            => ResetSelection();

        public void ResetSelection()
        {
            _description.ResetDescription();
            DeselectAllItems();
        }

        public void UpdateDescription(int index, Sprite icon, string name, string description)
        {
            _description.SetDescription(icon, name, description);
            DeselectAllItems();
            _itemViews[index].Select();
        }

        public void InitializeInventoryUI(int inventorySize)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                var uiItem = Instantiate(_itemPrefab, _contentPanel.transform);
                _itemViews.Add(uiItem);
                SubscribeItemView(uiItem);
            }
        }

        public void UpdateData(int itemIndex, Sprite itemIcon, int itemQuantity)
        {
            if (_itemViews.Count > itemIndex)
                _itemViews[itemIndex].SetData(itemIcon, itemQuantity);
        }

        public void ShowActionPanelByIndex(int index)
        {
            _actionPanel.Toggle(true);
            _actionPanel.transform.position = _itemViews[index].transform.position;
        }

        private void SubscribeItemView(ItemView uiItem)
        {
            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnRightMouseClicked += HandleShowItemActions;
        }

        private void DeselectAllItems()
        {
            foreach (var item in _itemViews)
                item.Deselect();

            _actionPanel.Toggle(false);
        }

        private void HandleItemSelection(ItemView item)
        {
            if (IsExist(item, out var index)) return;

            OnDescriptionRequested?.Invoke(index);
        }

        private bool IsExist(ItemView item, out int index)
        {
            index = _itemViews.IndexOf(item);
            return index == -1;
        }

        private void HandleShowItemActions(ItemView item)
        {
            if (IsExist(item, out var index))
                return;
            
            OnItemActionRequested?.Invoke(index);
        }

        public void AddAction(string name, IActionListener listener) 
            => _actionPanel.AddButton(name,listener);

        public void ResetAllItems()
        {
            foreach (var item in _itemViews)
            {
                item.CleanUp();
                item.Deselect();
            }
        }
    }
}