using UnityEngine;

namespace Logic.Inventory.Item
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 0)]
    public class ItemData : ScriptableObject
    {
        public Sprite Icon;
        public bool IsStackable;
        public int Id;
        public int MaxStackSize;
        public string Name;
        public string Description;
    }
}