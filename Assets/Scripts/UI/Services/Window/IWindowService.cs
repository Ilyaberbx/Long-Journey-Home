using System;
using System.Threading.Tasks;
using Infrastructure.Services;
using Infrastructure.StateMachine;
using Logic.Inventory;
using UI.Elements;
using UI.Ending;

namespace UI.Services.Window
{
    public interface IWindowService : IService
    {
        Task<WindowBase> Open(WindowType windowType, Action onClose = null);
        void Init(InventoryPresenter heroInventoryPresenter);
        Task<EndingWindow> OpenEndingWindow(EndingType ending);
    }
}