using System.Threading.Tasks;
using Infrastructure.Services;
using UI.Elements;
using UI.Envelope;
using UI.Inventory;
using UI.Menu;
using UI.Pause;
using UI.Settings;
using UnityEngine;

namespace UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        Task<InventoryWindow> CreateInventory();
        Task CreateUIRoot();
        Task<MenuWindow> CreateMainMenu();
        Task<SettingsWindow> CreateSettingsWindow();
        Task<PauseWindow> CreatePauseMenu();
        Task<GameObject> CreateEyeCurtain();
        Task<EnvelopeWindow> CreateEnvelopeWindow();
    }
}