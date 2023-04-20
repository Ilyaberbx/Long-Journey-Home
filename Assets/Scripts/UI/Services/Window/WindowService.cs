using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Logic.Inventory;
using UI.Elements;
using UI.Inventory;
using UI.Services.Factory;
using UI.Settings;
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

        public async Task<WindowBase> Open(WindowType windowType, Action onClose = null)
        {
            WindowBase window = null;

            switch (windowType)
            {
                case WindowType.None:
                    break;
                case WindowType.Inventory:
                    InventoryWindow inventoryView = await _uiFactory.CreateInventory();
                    inventoryView.SubscribeCloseListener(onClose);
                    _heroInventoryAdapter.InitUI(inventoryView);
                    window = inventoryView;
                    break;
                case WindowType.MainMenu:
                    window = await _uiFactory.CreateMainMenu();
                    break;
                case WindowType.Settings:
                    SettingsWindow settingsView = await _uiFactory.CreateSettingsWindow();
                    settingsView.SubscribeCloseListener(onClose);
                    window = settingsView;
                    break;
                default:
                    Debug.LogError("There is no type behaviour for this type: " + windowType);
                    break;
            }
            
            return window;
        }
        
    }
}