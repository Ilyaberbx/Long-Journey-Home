using Infrastructure.Services.Dialogue;
using UnityEngine;
using Zenject;

namespace Logic.DialogueSystem
{
    public class DialogueActor : MonoBehaviour, IDialogueActor
    {
        private IDialogueService _dialogueService;

        [Inject]
        public void Construct(IDialogueService dialogueService) 
            => _dialogueService = dialogueService;

        public void StartDialogue(Dialogue dialogue) 
            => _dialogueService.StartDialogue(dialogue);
    }
}