using System;
using Logic.Inventory.Item;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class LootSpawnerData
    {
        public string Id;
        public ItemData Data;
        public Vector3 Position;
        public Quaternion Rotation;


        public LootSpawnerData(string id, ItemData data,Vector3 position,Quaternion rotation)
        {
            Id = id;
            Data = data;
            Position = position;
            Rotation = rotation;
        }
    }
}