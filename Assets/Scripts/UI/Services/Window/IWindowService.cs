using System;
using System.Threading.Tasks;
using Infrastructure.Services;
using Logic.Inventory;
using UI.Elements;

namespace UI.Services.Window
{
    public interface IWindowService : IService
    {
        Task<WindowBase> Open(WindowType windowType, Action onClose = null);
        void Init(InventoryAdapter heroInventoryAdapter);
    }
}