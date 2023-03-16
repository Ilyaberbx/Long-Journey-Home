using Infrastructure.Interfaces;

namespace UI.Services.Window
{
    public interface IWindowService : IService
    {
        void Open(WindowType windowType);
    }
}