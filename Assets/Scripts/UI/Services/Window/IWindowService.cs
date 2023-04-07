using Infrastructure.Interfaces;
using Logic.Inventory;

namespace UI.Services.Window
{
    public interface IWindowService : IService
    {
        void Open(WindowType windowType,IActionListener closeListener);
        void Init(InventoryViewHandler heroInventoryViewHandler);
    }
}