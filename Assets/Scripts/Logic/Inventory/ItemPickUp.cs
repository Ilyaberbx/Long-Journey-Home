using Logic.Player;
using UnityEngine;

namespace Logic.Inventory
{
    public class ItemPickUp : MonoBehaviour, IInteractable
    {
        [SerializeField] private int _quantity;
        [SerializeField] private ItemData _itemData;
        [SerializeField] private string _interactText;

        public void Interact(Transform interactorTransform)
        {
            IHeroItemPicker picker = interactorTransform.GetComponent<IHeroItemPicker>();
            
            if (picker == null)
                return;
            
            if (TryPickUp(picker, out int reminder))
                Destroy(gameObject);
            else
                _quantity = reminder;
        }

        private bool TryPickUp(IHeroItemPicker picker, out int reminder) =>
            picker.TryPickUpItem(_itemData, _quantity, out reminder);

        public string GetInteractText()
            => _interactText;
    }
}