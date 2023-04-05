﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Logic.Inventory
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/InventoryData", order = 0)]
    public class InventoryData : ScriptableObject
    {
        [SerializeField] private List<InventoryItem> _inventoryItems;
        [SerializeField] private int _size;

        public int Size => _size;

        public void Init()
        {
            _inventoryItems = new List<InventoryItem>();

            for (int i = 0; i < _size; i++)
                _inventoryItems.Add(InventoryItem.GetEmptyItem());
        }

        public int AddItem(ItemData itemData, int quantity)
        {
            if (!itemData.IsStackable)
            {
                for (int i = 0; i < _inventoryItems.Count; i++)
                {
                    while (quantity > 0 && !IsInventoryFull()) 
                        quantity -= AddToFirstSlot(itemData, 1);

                    InformStateChanged();
                    return quantity;
                }
            }

            quantity = AddStackableItem(itemData, quantity);
            
            InformStateChanged();
            return quantity;
        }

        private int AddToFirstSlot(ItemData itemData, int quantity)
        {
            InventoryItem newItem = new InventoryItem
            {
                ItemData = itemData,
                Quantity = quantity
            };

            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                if (!_inventoryItems[i].IsEmpty) continue;
                
                _inventoryItems[i] = newItem;
                return quantity;
            }

            return 0;
        }

        private bool IsInventoryFull()
            => _inventoryItems.All(item => !item.IsEmpty);

        private int AddStackableItem(ItemData itemData, int quantity)
        {
            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                if(_inventoryItems[i].IsEmpty)
                    continue;

                if (_inventoryItems[i].ItemData.Id != itemData.Id) continue;
                
                int amountPossibleToTake = _inventoryItems[i].ItemData.MaxStackSize - _inventoryItems[i].Quantity;

                if (quantity > amountPossibleToTake)
                {
                    _inventoryItems[i] =
                        _inventoryItems[i].ChangeQuantity(_inventoryItems[i].ItemData.MaxStackSize);
                    quantity -= amountPossibleToTake;
                }
                else
                {
                    _inventoryItems[i] =
                        _inventoryItems[i].ChangeQuantity(_inventoryItems[i].Quantity + quantity);
                    InformStateChanged();
                    return 0;
                }
            }

            while (quantity > 0 && !IsInventoryFull())
            {
                int newQuantity = Mathf.Clamp(quantity, 0, itemData.MaxStackSize);
                quantity -= newQuantity;
                AddToFirstSlot(itemData, newQuantity);
            }

            return quantity;
        }

        private void InformStateChanged()
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> value = new Dictionary<int, InventoryItem>();

            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                if (_inventoryItems[i].IsEmpty)
                    continue;

                value[i] = _inventoryItems[i];
            }

            return value;
        }

        public InventoryItem GetItemByIndex(int index)
            => _inventoryItems[index];
    }
}