using System;
using System.Collections.Generic;
using UI.Elements;
using UnityEngine;

namespace UI.Inventory
{
    public class InventoryWindow : WindowBase
    {
        public event Action<int> OnDescriptionRequested, OnItemActionRequested;

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
            _description.SetDescription(icon,name,description);
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

        private void SubscribeItemView(ItemView uiItem)
        {
            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnRightMouseClicked += HandleShowItemActions;
        }

        private void DeselectAllItems()
        {
            foreach (var item in _itemViews) 
                item.Deselect();
        }

        private void HandleItemSelection(ItemView item)
        {
            int index = _itemViews.IndexOf(item);
            if(index == -1)
                return;
            
            OnDescriptionRequested?.Invoke(index);
        }

        private void HandleShowItemActions(ItemView item)
        {
        }
    }
}