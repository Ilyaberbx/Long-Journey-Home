using UnityEngine;

namespace Logic.Inventory
{
    public interface IItemAction
    {
        public string ActionName { get; }
        public AudioClip ActionSfx { get; }
        void PerformAction(GameObject character);
    }
}