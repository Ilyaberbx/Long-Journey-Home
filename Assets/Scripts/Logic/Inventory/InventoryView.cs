using UnityEngine;
using UnityEngine.Serialization;

namespace Logic.Inventory
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private GameObject _uiContainer;
        [SerializeField] private GameObject _uiSlot;

        private IInventory _inventory;

        public void SetInventory(IInventory inventory)
        {
            _inventory = inventory;
            RefreshInventoryItems();
        }

        private void RefreshInventoryItems()
        {
            foreach (var item in _inventory.GetItems())
            {
                RectTransform itemSlotRectTransform =
                    Instantiate(_uiSlot, _uiContainer.transform).GetComponent<RectTransform>();
                itemSlotRectTransform.gameObject.SetActive(true);
            }
        }
    }
}