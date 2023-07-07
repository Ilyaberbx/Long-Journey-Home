using System;
using Logic.Player;
using UnityEngine;

namespace Logic.Inventory.Item
{
    public class ItemPickUp : MonoBehaviour, IInteractable
    {
        public event Action OnPickedUp;
        [SerializeField] private int _quantity;
        [SerializeField] private ItemData _itemData;
        [SerializeField] private string _interactText;

        public void Interact(Transform interactor)
        {
            IItemPicker picker = interactor.GetComponent<IItemPicker>();
            
            if (picker == null)
                return;

            if (TryPickUp(picker, out int reminder))
            {
                OnPickedUp?.Invoke();
                Destroy(gameObject);
            }
            else
            {
                OnPickedUp?.Invoke();
                _quantity = reminder;
            }
        }

        private bool TryPickUp(IItemPicker picker, out int reminder) =>
            picker.TryPickUpItem(_itemData, _quantity, out reminder);

        public string GetInteractText()
            => _interactText;
    }
}