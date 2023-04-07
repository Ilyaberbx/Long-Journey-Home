using Infrastructure.Interfaces;
using UI.Inventory;

namespace UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        InventoryWindow CreateInventory();
        void CreateUIRoot();
    }
}