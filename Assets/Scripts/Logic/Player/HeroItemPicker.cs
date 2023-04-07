using Logic.Inventory;
using UnityEngine;

namespace Logic.Player
{
    public class HeroItemPicker : MonoBehaviour, IHeroItemPicker
    {
        [SerializeField] private InventoryData _inventoryData;

        public bool TryPickUpItem(ItemData itemData, int quantity, out int reminder)
        {
            reminder = _inventoryData.AddItem(itemData, quantity);

            if (reminder == 0)
                return true;
            
            return false;
        }
    }
}