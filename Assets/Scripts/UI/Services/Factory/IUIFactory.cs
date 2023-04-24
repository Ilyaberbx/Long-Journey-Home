using System.Threading.Tasks;
using Infrastructure.Services;
using UI.Elements;
using UI.Inventory;
using UI.Menu;
using UI.Settings;

namespace UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        Task<InventoryWindow> CreateInventory();
        Task CreateUIRoot();
        Task<MenuWindow> CreateMainMenu();
        Task<SettingsWindow> CreateSettingsWindow();
        Task<WindowBase> CreatePauseMenu();
    }
}