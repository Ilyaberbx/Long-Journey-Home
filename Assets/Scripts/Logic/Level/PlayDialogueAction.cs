using System.Collections;
using Infrastructure.Services.Dialogue;
using Logic.DialogueSystem;
using UnityEngine;
using Zenject;

namespace Logic.Level
{
    public class PlayDialogueAction : MonoBehaviour, IAction
    {
        [SerializeField] private float _delay = 0;
        [SerializeField] private Dialogue _dialogue;
        private IDialogueService _dialogueService;
        private bool _isPlayed;
        
        [Inject]
        public void Construct(IDialogueService dialogueService) 
            => _dialogueService = dialogueService;

        public void Execute()
        {
            if(_isPlayed)
                return;
            
            StartCoroutine(PlayDialogueRoutine());
        }

        private IEnumerator PlayDialogueRoutine()
        {
            yield return new WaitForSeconds(_delay);
            _isPlayed = true;
            _dialogueService.StartDialogue(_dialogue);
        }
    }
}