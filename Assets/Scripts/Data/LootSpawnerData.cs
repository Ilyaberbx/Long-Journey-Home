using System;
using Logic.Inventory.Item;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class LootSpawnerData
    {
        public string Id;
        public ItemPickUp Prefab;
        public Vector3 Position;
        public Quaternion Rotation;


        public LootSpawnerData(string id, ItemPickUp prefab,Vector3 position,Quaternion rotation)
        {
            Id = id;
            Prefab = prefab;
            Position = position;
            Rotation = rotation;
        }
    }
}