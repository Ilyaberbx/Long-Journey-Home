using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Logic.Player.Inventory
{
    public class InventoryWithSlots : IInventory
    {
        public event Action<IInventoryItem, int> OnInventoryItemAdded;
        public event Action<Type, int> OnInventoryItemRemoved;

        public int Capacity { get; }
        public bool IsFull => _slots.All(slot => slot.IsFull);

        private List<IInventorySlot> _slots;


        public InventoryWithSlots(int capacity)
        {
            Capacity = capacity;
            _slots = new List<IInventorySlot>();

            InitSlots();
        }

        private void InitSlots()
        {
            for (int i = 0; i < Capacity; i++)
                _slots.Add(new InventorySlot());
        }

        public IInventoryItem GetItem(Type itemType)
            => _slots.Find(slot => slot.ItemType == itemType).Item;

        public IInventoryItem[] GetAllItems()
            => (from slot in _slots where !slot.IsEmpty select slot.Item).ToArray();

        public IInventoryItem[] GetEquippedItems()
            => (from slot in _slots where !slot.IsEmpty && slot.Item.IsEquipped select slot.Item).ToArray();

        public IInventoryItem[] GetAllItems(Type type)
            => (from slot in _slots where !slot.IsEmpty && slot.ItemType == type select slot.Item).ToArray();

        public int GetItemAmount(Type type)
        {
            var items =
                (from slot in _slots where !slot.IsEmpty && slot.ItemType == type select slot.Item).ToList();

            return items.Sum(item => item.Amount);
        }

        public bool TryAdd(IInventoryItem item)
        {
            var slotWithSameItemButNotEmpty = _slots.Find(
                slot => !slot.IsEmpty && !slot.IsFull && slot.ItemType == item.Type);

            if (slotWithSameItemButNotEmpty != null)
                return TryAddToSlot(slotWithSameItemButNotEmpty, item);

            var emptySlot = _slots.Find(slot => slot.IsEmpty);

            if (emptySlot != null)
                return TryAddToSlot(emptySlot, item);

            Debug.Log("Can not add , no space for item");
            return false;
        }

        private bool TryAddToSlot(IInventorySlot slot, IInventoryItem item)
        {
            bool fits = slot.Amount + item.Amount <= item.MaxItemsInInventorySlot;

            int amountToAdd = fits
                ? item.Amount
                : item.MaxItemsInInventorySlot - slot.Amount;

            int amountLeft = item.Amount - amountToAdd;

            var clonedItem = item.Clone();
            clonedItem.Amount = amountToAdd;

            if (slot.IsEmpty)
                slot.SetItem(clonedItem);
            else
                slot.Item.Amount += amountToAdd;

            Debug.Log($"Item Added to inventory. Item type: {item.Type}, Amount to add: {amountToAdd}");
            OnInventoryItemAdded?.Invoke(item, amountToAdd);

            if (amountLeft <= 0)
                return true;

            item.Amount = amountLeft;
            return TryAdd(item);
        }

        public void Remove(Type type, int amount = 1)
        {
            var slotsWithItem = GetAllSlots(type);

            if (slotsWithItem.Length == 0)
            {
                Debug.Log("No slots");
                return;
            }

            int amountToRemove = amount;
            int count = slotsWithItem.Length;


            for (int i = count - 1; i >= 0; i--)
            {
                var slot = slotsWithItem[i];
                if (slot.Amount >= amountToRemove)
                {
                    slot.Item.Amount -= amountToRemove;

                    if (slot.Amount <= 0)
                        slot.Clear();

                    Debug.Log($"Item Removed to inventory. Item type: {type}, Amount to remove: {amountToRemove}");
                    OnInventoryItemRemoved?.Invoke(type, amountToRemove);

                    break;
                }

                var amountRemoved = slot.Amount;
                amountToRemove -= slot.Amount;
                slot.Clear();
                Debug.Log($"Item Removed to inventory. Item type: {type}, Amount to remove: {amountRemoved}");
                OnInventoryItemRemoved?.Invoke(type, amountRemoved);
            }
        }

        private IInventorySlot[] GetAllSlots(Type type)
            => _slots.FindAll(slot => !slot.IsEmpty && slot.ItemType == type).ToArray();

        public IInventorySlot[] GetAllSlots()
            => _slots.ToArray();

        public bool HasItem(Type type, out IInventoryItem item)
        {
            item = GetItem(type);
            return item == null;
        }
    }
}