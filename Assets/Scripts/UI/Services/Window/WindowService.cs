using Logic.Inventory;
using UI.Inventory;
using UI.Services.Factory;
using UnityEngine;

namespace UI.Services.Window
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;
        private InventoryViewHandler _heroInventoryViewHandler;

        public WindowService(IUIFactory uiFactory) 
            => _uiFactory = uiFactory;

        public void Init(InventoryViewHandler heroInventoryViewHandler) 
            => _heroInventoryViewHandler = heroInventoryViewHandler;

        public void Open(WindowType windowType, IActionListener closelistener)
        {
            switch (windowType)
            {
                case WindowType.None:
                    break;
                case WindowType.Inventory:
                   InventoryWindow window = _uiFactory.CreateInventory();
                   window.SubscribeCloseListener(closelistener);
                   _heroInventoryViewHandler.InitUI(window);
                    break;
                default:
                    Debug.LogError("There is no type behaviour for this type: " + windowType);
                    break;
            }
        }
    }
}