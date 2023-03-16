using Logic.Player.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Inventory
{
    public class UIInventorySlot : UISlot
    {
        public IInventorySlot Slot { get; private set; }

        [SerializeField] private UIInventoryItem _uiInventoryItem;
        private InventoryWindow _inventoryWindow;

        private void Awake() 
            => _inventoryWindow = GetComponentInParent<InventoryWindow>();

        public void SetSlot(IInventorySlot newSlot) 
            => Slot = newSlot;

        public override void OnDrop(PointerEventData eventData)
        {
            UIInventoryItem UIItem = eventData.pointerDrag.GetComponent<UIInventoryItem>();
            UIInventorySlot UIItemSlot = UIItem.GetComponentInParent<UIInventorySlot>();
            IInventorySlot itemSlot = UIItemSlot.Slot;
            InventoryWithSlots inventory = _inventoryWindow.Inventory;
            
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