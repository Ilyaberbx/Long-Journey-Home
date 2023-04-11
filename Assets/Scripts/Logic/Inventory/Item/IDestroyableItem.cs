using System;

namespace Logic.Inventory.Item
{
    public interface IDestroyableItem
    {
        public event Action OnDrop;
        void Drop();
    }
}