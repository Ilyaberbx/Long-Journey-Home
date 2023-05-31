using UnityEngine;

namespace Logic.Inventory.Item
{
    public abstract class BaseEquippableItem : MonoBehaviour
    {
        public abstract void Appear();

        public abstract void Hide();
    }
}