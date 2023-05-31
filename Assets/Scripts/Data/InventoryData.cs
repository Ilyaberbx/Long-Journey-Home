using System;
using System.Collections.Generic;
using System.Linq;
using Logic.Inventory.Item;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    [Serializable]
    public class InventoryData
    {
        public event Action OnStateChanged;
        public List<InventoryItem> InventoryItems;

        public void Init(int size)
        {
            InventoryItems = new List<InventoryItem>();

            for (int i = 0; i < size; i++)
                InventoryItems.Add(InventoryItem.GetEmptyItem());
        }

        public int GetSize()
            => InventoryItems.Count;

        public int AddItem(ItemData itemData, int quantity)
        {
            if (!itemData.IsStackable)
            {
                for (int i = 0; i < InventoryItems.Count; i++)
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

        public bool IsEmpty()
            => InventoryItems.All(item => item.IsEmpty);

        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> value = new Dictionary<int, InventoryItem>();

            for (int i = 0; i < InventoryItems.Count; i++)
            {
                if (InventoryItems[i].IsEmpty)
                    continue;

                value[i] = InventoryItems[i];
            }

            return value;
        }

        public InventoryItem GetItemByIndex(int index)
            => InventoryItems[index];


        public bool HasItemById(int id)
        {
            for (int i = 0; i < InventoryItems.Count; i++)
            {
                if (InventoryItems[i].IsEmpty)
                    continue;
                
                if (InventoryItems[i].ItemData.Id == id)
                    return true;
            }
            return false;
        }
        
        public bool TryRemoveItemById(int id, int amount)
        {
            for (int i = 0; i < InventoryItems.Count; i++)
            {
                if (InventoryItems[i].IsEmpty)
                    continue;

                if (InventoryItems[i].ItemData.Id != id)
                    continue;

                if (InventoryItems[i].Quantity < amount)
                    continue;

                RemoveItemByIndex(i, amount);
                return true;
            }

            return false;
        }

        public void RemoveItemByIndex(int index, int amount)
        {
            if (InventoryItems.Count > index)
            {
                if (InventoryItems[index].IsEmpty) return;

                int reminder = InventoryItems[index].Quantity - amount;

                if (reminder <= 0)
                    InventoryItems[index] = InventoryItem.GetEmptyItem();
                else
                    InventoryItems[index] = InventoryItems[index]
                        .ChangeQuantity(reminder);

                InformStateChanged();
            }
        }


        private int AddToFirstSlot(ItemData itemData, int quantity)
        {
            InventoryItem newItem = new InventoryItem
            {
                ItemData = itemData,
                Quantity = quantity
            };

            for (int i = 0; i < InventoryItems.Count; i++)
            {
                if (!InventoryItems[i].IsEmpty) continue;

                InventoryItems[i] = newItem;
                return quantity;
            }

            return 0;
        }

        private bool IsInventoryFull()
            => InventoryItems.All(item => !item.IsEmpty);

        private int AddStackableItem(ItemData itemData, int quantity)
        {
            for (int i = 0; i < InventoryItems.Count; i++)
            {
                if (InventoryItems[i].IsEmpty)
                    continue;

                if (InventoryItems[i].ItemData.Id != itemData.Id)
                    continue;

                int amountPossibleToTake = InventoryItems[i].ItemData.MaxStackSize - InventoryItems[i].Quantity;

                if (quantity > amountPossibleToTake)
                {
                    InventoryItems[i] =
                        InventoryItems[i].ChangeQuantity(InventoryItems[i].ItemData.MaxStackSize);
                    quantity -= amountPossibleToTake;
                }
                else
                {
                    InventoryItems[i] =
                        InventoryItems[i].ChangeQuantity(InventoryItems[i].Quantity + quantity);
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
            => OnStateChanged?.Invoke();
    }
}