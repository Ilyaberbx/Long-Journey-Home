using DG.Tweening;
using Extensions;
using Logic.DialogueSystem;
using Logic.Inventory;
using Logic.Inventory.Item;
using Logic.Player;
using UnityEngine;

namespace Logic.Level
{
    public class Door : MonoBehaviour,IInteractable
    {
        [SerializeField] private GameObject _key;
        [SerializeField] private ItemData _keyData;
        [SerializeField] private Dialogue _noKeyDialogue;
        [SerializeField] private string _interactText;
        private bool _isOpened;

        public void Interact(Transform interactor)
        {
            if (_isOpened)
                return;
            
            if (TryGetInventory(interactor, out InventoryPresenter inventory))
            {
                if (inventory.HasItem(_keyData.Id))
                {
                    Open();
                    _key.SetActive(true);
                    _isOpened = true;
                }
                else
                    NotifyNoKey(interactor);
            }
        }

        private void NotifyNoKey(Transform interactor) 
            => interactor.GetComponent<DialogueActor>().StartDialogue(_noKeyDialogue);

        private bool TryGetInventory(Transform interactor, out InventoryPresenter inventory) 
            => interactor.TryGetComponent(out inventory);

        private void Open() 
            => transform.DOLocalRotate(Vector3.zero.AddY(90f), 1f);

        public string GetInteractText() 
            => _interactText;
    }
}