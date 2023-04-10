using System;

namespace Logic.Inventory
{
    public interface IDestroyableItem
    {
        public event Action OnDrop;
        void Drop();
    }
}