using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "ItemsConfig", fileName = "ItemsConfig", order = 0)]
    public class ItemsConfig : ScriptableObject
    {
        [SerializeField] private List<ItemData> _itemsData;

        public List<ItemData> ItemsData => _itemsData;
    }
}