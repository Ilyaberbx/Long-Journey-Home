using System;
using Logic.Inventory;
using Logic.Inventory.Actions;
using UI.Inventory;
using UI.Menu;
using UI.Services.Factory;
using UnityEditor.VersionControl;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

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

        public async Task Open(WindowType windowType, Action onClose = null)
        {
            switch (windowType)
            {
                case WindowType.None:
                    break;
                case WindowType.Inventory:
                    InventoryWindow inventoryView = await _uiFactory.CreateInventory();
                    inventoryView.SubscribeCloseListener(onClose);
                    _heroInventoryAdapter.InitUI(inventoryView);
                    break;
                case WindowType.MainMenu:
                    await _uiFactory.CreateMainMenu();
                    break;
                default:
                    Debug.LogError("There is no type behaviour for this type: " + windowType);
                    break;
            }
        }
    }
}