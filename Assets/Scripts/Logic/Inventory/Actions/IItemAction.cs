using UnityEngine;

namespace Logic.Inventory.Actions
{
    public interface IItemAction
    {
        public string ActionName { get; }
        public AudioClip ActionSfx { get; }
        void PerformAction(GameObject character);
    }
}