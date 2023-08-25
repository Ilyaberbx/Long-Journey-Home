using Infrastructure.Services.Dialogue;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI.Elements
{
    public class DialogueView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _dialogueText;
        private IDialogueService _dialogueService;

        [Inject]
        public void Construct(IDialogueService dialogueService)
        {
            _dialogueService = dialogueService;
            
            _dialogueService.OnSentenceCleared += ClearDialogueText;
            _dialogueService.OnSentenceTyping += TypeDialogueText;
        }

        private void ClearDialogueText() 
            => _dialogueText.text = "";

        private void TypeDialogueText(char letter) 
            => _dialogueText.text += letter;
        
    }
}
