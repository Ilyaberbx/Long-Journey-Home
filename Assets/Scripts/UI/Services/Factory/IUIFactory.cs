using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using UI.Inventory;
using UI.Menu;

namespace UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        Task<InventoryWindow> CreateInventory();
        Task CreateUIRoot();
        Task<MenuWindow> CreateMainMenu();
    }
}