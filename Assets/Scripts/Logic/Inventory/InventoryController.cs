using UnityEngine;

namespace Logic.Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private InventoryView _inventoryView;
        private IInventory _inventory;
        private void Awake()
        {
            _inventory = new Inventory();
            _inventoryView.SetInventory(_inventory);
        }
    }
}