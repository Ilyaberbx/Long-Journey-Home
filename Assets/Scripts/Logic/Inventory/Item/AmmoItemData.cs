using Logic.Player;
using UnityEngine;

namespace Logic.Inventory.Item
{
    [CreateAssetMenu(fileName = "Ammo", menuName = "Inventory/Ammo", order = 0)]
    public class AmmoItemData : ItemData, IReducible
    {
    }
}