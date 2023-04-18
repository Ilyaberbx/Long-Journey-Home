using Logic.Inventory;
using Logic.Inventory.Actions;
using UI.Inventory;
using UI.Services.Factory;
using UnityEngine;

namespace UI.Services.Window
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;
        private InventoryAdapter _heroInventoryAdapter;

        public WindowService(IUIFactory uiFactory) 
            => _uiFactory = uiFactory;

        public void Init(InventoryAdapter heroInventoryAdapter) 
            => _heroInventoryAdapter = heroInventoryAdapter;

        public async void Open(WindowType windowType, IActionListener closelistener)
        {
            switch (windowType)
            {
                case WindowType.None:
                    break;
                case WindowType.Inventory:
                   InventoryWindow window = await _uiFactory.CreateInventory();
                   window.SubscribeCloseListener(closelistener);
                   _heroInventoryAdapter.InitUI(window);
                    break;
                default:
                    Debug.LogError("There is no type behaviour for this type: " + windowType);
                    break;
            }
        }
    }
}