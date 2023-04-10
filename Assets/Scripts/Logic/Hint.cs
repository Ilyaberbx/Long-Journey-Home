using System.Collections.Generic;
using Logic.DialogueSystem;
using Logic.Inventory;
using Logic.Player;
using UnityEngine;

namespace Logic
{
    public class Hint : MonoBehaviour, IInteractable
    {
        [SerializeField] private List<ItemPickUp> _itemPickUps;
        [SerializeField] private Dialogue _dialogue;
        [SerializeField] private string _hintName;

        public void Interact(Transform interactorTransform)
        {
            IDialogueActor dialogueActor = interactorTransform.GetComponent<IDialogueActor>();
            dialogueActor.StartDialogue(_dialogue);

            foreach (var itemPickUp in _itemPickUps) 
                itemPickUp.Interact(interactorTransform);
        }

        public string GetInteractText()
            => _hintName;
    }
}