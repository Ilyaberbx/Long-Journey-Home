using System.Collections.Generic;
using Logic.Player.Inventory;
using StaticData.Inventory;
using UnityEngine;

namespace UI
{
    public class UIInventoryTester
    {
        private readonly UIInventorySlot[] _uiSlots;
        private readonly InventoryItemInfo _pepperInfo;
        private readonly InventoryItemInfo _appleInfo;
        
        public InventoryWithSlots Inventory { get; }

        public UIInventoryTester(InventoryItemInfo appleInfo, InventoryItemInfo pepperInfo, UIInventorySlot[] uiSlots)
        {
            _appleInfo = appleInfo;
            _pepperInfo = pepperInfo;
            _uiSlots = uiSlots;

            Inventory = new InventoryWithSlots(9);
            Inventory.OnStateChanged += DrawUI;
        }

        public void FillSlots()
        {
            var allSlots = Inventory.GetAllSlots();
            var availableSlots = new List<IInventorySlot>(allSlots);

            var filledSlotCount = 2;

            for (int i = 0; i < filledSlotCount; i++)
            {
                var filledSlot = AddRandApples(availableSlots);
                availableSlots.Remove(filledSlot);

                filledSlot = AddRandPeppers(availableSlots);
                availableSlots.Remove(filledSlot);
            }

            SetUpInventoryUI(Inventory);
        }

        private IInventorySlot AddRandApples(List<IInventorySlot> slots)
        {
            var randSlot = slots[Random.Range(0, slots.Count)];
            var randCount = Random.Range(1, 4);
            var apple = new Apple(_appleInfo);
            apple.State.Amount = randCount;
            Inventory.TryAddToSlot(randSlot, apple);
            return randSlot;
        }
        private IInventorySlot AddRandPeppers(List<IInventorySlot> slots)
        {
            var randSlot = slots[Random.Range(0, slots.Count)];
            var randCount = Random.Range(1, 4);
            var pepper = new Pepper(_pepperInfo);
            pepper.State.Amount = randCount;
            Inventory.TryAddToSlot(randSlot, pepper);
            return randSlot;
        }

        private void SetUpInventoryUI(InventoryWithSlots inventory)
        {
            var allSlots = Inventory.GetAllSlots();
            var count = allSlots.Length;

            for (int i = 0; i < count; i++)
            {
                var slot = allSlots[i];
                var uiSlot = _uiSlots[i];
                uiSlot.SetSlot(slot);
                uiSlot.Refresh();
            }
        }
        
        private void DrawUI()
        {
            foreach (var slot in _uiSlots) 
                slot.Refresh();
        }
    }
}