using System;
using System.Threading.Tasks;
using Infrastructure.StateMachine;
using Logic.Inventory;
using UI.Elements;
using UI.Ending;
using UI.Envelope;
using UI.Inventory;
using UI.Services.Factory;
using UI.Settings;
using UnityEngine;

namespace UI.Services.Window
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;
        private InventoryPresenter _heroInventoryPresenter;

        public WindowService(IUIFactory uiFactory)
            => _uiFactory = uiFactory;

        public void Init(InventoryPresenter heroInventoryPresenter)
            => _heroInventoryPresenter = heroInventoryPresenter;

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
                    _heroInventoryPresenter.InitUI(inventoryView);
                    window = inventoryView;
                    break;
                case WindowType.MainMenu:
                    window = await _uiFactory.CreateMainMenu();
                    break;
                case WindowType.Pause:
                    window = await _uiFactory.CreatePauseMenu();
                    window.SubscribeCloseListener(onClose);
                    break;
                case WindowType.Settings:
                    SettingsWindow settingsView = await _uiFactory.CreateSettingsWindow();
                    settingsView.SubscribeCloseListener(onClose);
                    window = settingsView;
                    break;
                case WindowType.Envelope:
                    EnvelopeWindow envelopeView = await _uiFactory.CreateEnvelopeWindow();
                    envelopeView.SubscribeCloseListener(onClose);
                    window = envelopeView;
                    break;
                case WindowType.GameOver:
                    window = await _uiFactory.CreateGameOverMenu();
                    break;

                case WindowType.Ending:
                    window = await _uiFactory.CreateEndingWindow();
                    break;
                case WindowType.Tutorial:
                    window = await _uiFactory.CreateTutorialWindow();
                    break;
                default:
                    Debug.LogError("There is no type behaviour for this type: " + windowType);
                    break;
            }
            
            return window;
        }

        public async Task<EndingWindow> OpenEndingWindow(EndingType ending)
        {
            EndingWindow endingWindow = (EndingWindow)await Open(WindowType.Ending);
            endingWindow.UpdateUI(ending);
            return endingWindow;
        }
    }
}