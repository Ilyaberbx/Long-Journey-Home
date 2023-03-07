using System;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Logic.Player.Inventory
{
    public class Tester : MonoBehaviour
    {
        private IInventory _inventory;

        private void Awake()
        {
            _inventory = new InventoryWithSlots(10);
            Debug.Log("Inventory Created with 10 capacity");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
                AddRandomApples();
            
            if (Input.GetKeyDown(KeyCode.R))
                RemoveRandomApples();
        }

        private void RemoveRandomApples()
        {
            var randCount = Random.Range(1, 5);
            _inventory.Remove(typeof(Apple),randCount);
        }

        private void AddRandomApples()
        {
            var randCount = Random.Range(1, 5);
            var apple = new Apple(5)
            {
                Amount = randCount
            };
            _inventory.TryAdd(apple);
        }
    }
}