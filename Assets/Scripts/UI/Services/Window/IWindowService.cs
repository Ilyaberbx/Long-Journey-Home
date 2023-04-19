using System;
using System.Threading.Tasks;
using Infrastructure.Services;
using Logic.Inventory;

namespace UI.Services.Window
{
    public interface IWindowService : IService
    {
        Task Open(WindowType windowType, Action onClose = null);
        void Init(InventoryAdapter heroInventoryAdapter);
    }
}