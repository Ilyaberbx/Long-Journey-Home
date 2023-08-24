using Logic.DialogueSystem;
using TMPro;
using UnityEngine;

namespace UI.Elements
{

    public class DialogueView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _dialogueText;
        private IDialogueActor _actor;

        public void Init(IDialogueActor actor)
        {
            _actor = actor;
            
            _actor.OnSentenceCleared += ClearDialogueText;
            _actor.OnSentenceTyping += TypeDialogueText;
        }

        private void ClearDialogueText() 
            => _dialogueText.text = "";

        private void TypeDialogueText(char letter) 
            => _dialogueText.text += letter;
        
    }
}
