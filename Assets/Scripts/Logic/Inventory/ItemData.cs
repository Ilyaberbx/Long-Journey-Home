using UnityEngine;

namespace Logic.Inventory
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 0)]
    public abstract class ItemData : ScriptableObject
    {
        public Sprite Icon;
        public bool IsStackable;

        public int Id => GetInstanceID();
        public int MaxStackSize;
        public string Name;
        public string Description;
    }
}