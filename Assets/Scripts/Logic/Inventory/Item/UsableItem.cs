using System;
using System.Collections.Generic;
using Logic.Inventory.Actions;
using Logic.Inventory.Modifiers;
using Logic.Player;
using UnityEngine;

namespace Logic.Inventory.Item
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/UsableItem", order = 0)]
    public class UsableItem : ItemData, IItemAction,IReducible,IDestroyableItem
    {
        public event Action OnDrop;

        [SerializeField] private List<ModifierData> _modifiersData = new List<ModifierData>();
        [SerializeField] private AudioClip _actionSfx;
        public string ActionName => "Use";
        public AudioClip ActionSfx => _actionSfx;


        public void PerformAction(GameObject character)
        {
            foreach (var data in _modifiersData) 
                data.StatModifier.AffectCharacter(character, data.Value);
        }

        public void Drop() 
            => OnDrop?.Invoke();
    }
}