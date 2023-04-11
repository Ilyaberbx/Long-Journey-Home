using Infrastructure.Interfaces;
using Infrastructure.Services;
using UI.Inventory;

namespace UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        InventoryWindow CreateInventory();
        void CreateUIRoot();
    }
}