using Logic.DialogueSystem;
using Logic.Player;
using UnityEngine;

namespace Logic.Level
{
    public class Hint : MonoBehaviour, IInteractable
    {
        [SerializeField] private Dialogue _dialogue;
        [SerializeField] private string _hintName;

        public void Interact(Transform interactorTransform)
        {
            IDialogueActor dialogueActor = interactorTransform.GetComponent<IDialogueActor>();
            dialogueActor.StartDialogue(_dialogue);
        }

        public string GetInteractText()
            => _hintName;
    }
}