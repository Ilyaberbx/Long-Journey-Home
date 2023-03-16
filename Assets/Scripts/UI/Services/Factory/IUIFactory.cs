using Infrastructure.Interfaces;

namespace UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        void CreateInventory();
        void CreateUIRoot();
    }
}