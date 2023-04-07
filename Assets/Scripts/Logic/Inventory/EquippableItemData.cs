using UnityEngine;

namespace Logic.Inventory
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/EquippableItem", order = 0)]
    public class EquippableItemData : ItemData, IDestroyableItem, IItemAction
    {
        public string ActionName => "Equip";
        public AudioClip ActionSfx => _actionSfx;

        [SerializeField] private AudioClip _actionSfx;

        public void PerformAction(GameObject character)
        {
            Debug.Log("Equip");
        }
    }
}