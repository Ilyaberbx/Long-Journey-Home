using Logic.Player.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UIInventorySlot : UISlot
    {
        public IInventorySlot Slot { get; private set; }

        [SerializeField] private UIInventoryItem _uiInventoryItem;
        private UIInventory _uiInventory;

        private void Awake() 
            => _uiInventory = GetComponentInParent<UIInventory>();

        public void SetSlot(IInventorySlot newSlot) 
            => Slot = newSlot;

        public override void OnDrop(PointerEventData eventData)
        {
            UIInventoryItem UIItem = eventData.pointerDrag.GetComponent<UIInventoryItem>();
            UIInventorySlot UIItemSlot = UIItem.GetComponentInParent<UIInventorySlot>();
            IInventorySlot itemSlot = UIItemSlot.Slot;
            InventoryWithSlots inventory = _uiInventory.Inventory;
            
            inventory.TransitFromSlotToSlot(itemSlot, Slot);
            
            Refresh();
            UIItemSlot.Refresh();
        }

        public void Refresh()
        {
            if(Slot != null)
                _uiInventoryItem.Refresh(Slot);
        }
    }
}