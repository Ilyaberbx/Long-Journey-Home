using Logic.Player.Inventory;
using StaticData.Inventory;
using UI.Elements;
using UnityEngine;

namespace UI.Inventory
{
    public class InventoryWindow : WindowBase
    {

        [SerializeField] private InventoryItemInfo _appleInfo;
        [SerializeField] private InventoryItemInfo _pepperInfo;

        public InventoryWithSlots Inventory => _tester.Inventory;

        private UIInventoryTester _tester;

        private void Start()
        {
            var uiSlots = GetComponentsInChildren<UIInventorySlot>();
            _tester = new UIInventoryTester(_appleInfo,_pepperInfo,uiSlots);
            _tester.FillSlots();
        }
    }
}