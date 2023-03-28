using Data;

namespace Logic.Inventory
{
    public abstract class BaseItem
    {
        public ItemData Info { get; private set; }

        public void Init(ItemData info) 
            => Info = info;
    }
}