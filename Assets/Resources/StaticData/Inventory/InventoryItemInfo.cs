using Logic.Player.Inventory;
using UnityEngine;

namespace StaticData.Inventory
{
    [CreateAssetMenu(menuName = "Inventory",fileName = "Item")]
    public class InventoryItemInfo : ScriptableObject,IInventoryItemInfo
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _id;
        [SerializeField] private string _title;
        [SerializeField] private string _description;
        [SerializeField] private int _maxItemsInSlot;

        public Sprite Icon => _icon;
        public string Id => _id;
        public string Title => _title;
        public string Description => _description;
        public int MaxItemsInInventorySlot => _maxItemsInSlot;
    }
}