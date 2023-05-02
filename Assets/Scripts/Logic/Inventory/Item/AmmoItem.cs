using System;
using Logic.Player;
using UnityEngine;

namespace Logic.Inventory.Item
{
    [CreateAssetMenu(fileName = "Ammo", menuName = "Inventory/Ammo", order = 0)]
    public class AmmoItem : ItemData, IDestroyableItem, IReducible
    {
        public event Action OnDrop;

        public void Drop()
            => OnDrop?.Invoke();
    }
}