using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using UI.Inventory;

namespace UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        Task<InventoryWindow> CreateInventory();
        Task CreateUIRoot();
    }
}