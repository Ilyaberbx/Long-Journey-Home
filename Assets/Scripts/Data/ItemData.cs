using Logic.Inventory;
using UnityEngine;

namespace Data
{
    [System.Serializable]
    public class ItemData 
    {
        [SerializeField] private BaseItem _prefab;
        [SerializeField] private ItemType _type;
        [SerializeField] private Sprite _icon;
        [SerializeField] private int _maxStackCount;
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private bool _isEquippable;

        public BaseItem Prefab => _prefab;
        public bool IsEquippable => _isEquippable;
        public ItemType Type => _type;
        public Sprite Icon => _icon;
        public int MaxStackCount => _maxStackCount;
        public string Name => _name;
        public string Description => _description;
    }
}