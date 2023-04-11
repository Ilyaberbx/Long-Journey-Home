using Data;
using Infrastructure.Interfaces;
using Infrastructure.Services.SaveLoad;
using Logic.Inventory;
using Logic.Inventory.Item;
using UnityEngine;

namespace Logic.Player
{
    public class HeroItemPicker : MonoBehaviour, IItemPicker,ISavedProgressWriter
    {
        private InventoryData _inventoryData;

        public bool TryPickUpItem(ItemData itemData, int quantity, out int reminder)
        {
            reminder = _inventoryData.AddItem(itemData, quantity);

            if (reminder == 0)
                return true;
            
            return false;
        }

        public void LoadProgress(PlayerProgress progress) 
            => _inventoryData = progress.InventoryData;

        public void UpdateProgress(PlayerProgress progress) 
            => progress.InventoryData = _inventoryData;
    }
}