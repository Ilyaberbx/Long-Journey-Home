using Logic.Inventory;
using Logic.Player;
using UnityEngine;

namespace Logic
{
    public class ItemPickUp : MonoBehaviour,IInteractable
    {
        [SerializeField] private ItemType _type;
        [SerializeField] private string _pickUpText;
        

        public void Interact(Transform interactorTransform)
        {
            interactorTransform.GetComponent<IInventoryController>().AddItem(_type);
            Destroy(gameObject);
        }

        public string GetInteractText() 
            => _pickUpText;
    }
}