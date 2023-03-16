using UI.Services.Factory;
using UnityEngine;

namespace UI.Services.Window
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }
        
        public void Open(WindowType windowType)
        {
            switch (windowType)
            {
                case WindowType.None:
                    break;
                case WindowType.Inventory:
                    _uiFactory.CreateInventory();
                    break;
                default:
                    Debug.LogError("There is no type behaviour for this type: " + windowType);
                    break;
            }
        }
    }
}