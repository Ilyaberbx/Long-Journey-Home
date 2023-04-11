using System;
using Logic.Inventory.Actions;
using Logic.Player;
using UnityEngine;

namespace Logic.Inventory.Item
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/EquippableItem", order = 0)]
    public class EquippableItemData : ItemData, IDestroyableItem, IItemAction
    {
        public event Action OnDrop;

        [SerializeField] private BaseEquippableItem _itemPrefab;
        [SerializeField] private AudioClip _actionSfx;

        public string ActionName => "Equip";

        public BaseEquippableItem ItemPrefab => _itemPrefab;

        public AudioClip ActionSfx => _actionSfx;


        public void Drop()
            => OnDrop?.Invoke();

        public void PerformAction(GameObject character)
            => character.GetComponent<HeroEquiper>()
                .SelectEquipment(this);
    }
}