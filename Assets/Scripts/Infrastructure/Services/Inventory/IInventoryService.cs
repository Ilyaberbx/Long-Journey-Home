using Infrastructure.Interfaces;


namespace Infrastructure.Services.Inventory
{
    public interface IInventoryService : IService
    {
        public void AddItem();
        public void RemoveItem();
        public void UseItem();
    }
}