using Logic.Inventory;
using UnityEngine;

namespace Logic.Player
{
    public interface IEquippable
    {
        ItemType ItemType { get; }
        Transform GetTransform();
        void Appear();
    
    }
}