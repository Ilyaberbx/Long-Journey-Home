using Infrastructure.Interfaces;
using Infrastructure.Services;
using Logic.Inventory;
using Logic.Inventory.Actions;

namespace UI.Services.Window
{
    public interface IWindowService : IService
    {
        void Open(WindowType windowType,IActionListener closeListener);
        void Init(InventoryAdapter heroInventoryAdapter);
    }
}