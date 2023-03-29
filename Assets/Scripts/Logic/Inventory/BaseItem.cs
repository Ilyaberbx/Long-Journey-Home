using Logic.Player;
using UnityEngine;

namespace Logic.Inventory
{
    public abstract class BaseItem : MonoBehaviour
    {
        [SerializeField] private ItemType _type;
        protected ItemType Type => _type;
        public abstract void Use(HeroMover player);
    }
}