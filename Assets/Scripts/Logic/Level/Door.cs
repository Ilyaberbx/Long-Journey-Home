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
        [SerializeField] private Collider _interactCollider;
        [SerializeField] private GameObject _key;
        [SerializeField] private ItemData _keyData;
        [SerializeField] private Dialogue _noKeyDialogue;
        [SerializeField] private string _interactText;
        [SerializeField] private bool _requiersKey = true;
        [SerializeField] private float _openValue = 90f;
        private bool _isOpened;

        public void Interact(Transform interactor)
        {
            if (_isOpened)
                return;

            if (_requiersKey)
            {
                if (TryGetInventory(interactor, out InventoryPresenter inventory))
                {
                    if (inventory.HasItem(_keyData.Id))
                        Open();
                    else
                        NotifyNoKey(interactor);
                }
            }
            else
                Open();
        }

        private void Open()
        {
            OpenDoor();
            _key.SetActive(true);
            _interactCollider.enabled = false;
            _isOpened = true;
        }

        private void NotifyNoKey(Transform interactor) 
            => interactor.GetComponent<DialogueActor>().StartDialogue(_noKeyDialogue);

        private bool TryGetInventory(Transform interactor, out InventoryPresenter inventory) 
            => interactor.TryGetComponent(out inventory);

        private void OpenDoor() 
            => transform.DOLocalRotate(Vector3.zero.AddY(_openValue), 1f);

        public string GetInteractText() 
            => _interactText;
    }
}