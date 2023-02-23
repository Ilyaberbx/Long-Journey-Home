using Interfaces;
using Logic.DialogueSystem;
using Logic.Player;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerUIActor : UIActor
    {
        [SerializeField] private Bar _flashLightBar;
        [SerializeField] private TextMeshProUGUI _dialogueText;
        
        private FlashLight _flashLight;
        private IDialogueActor _dialogueActor;

        public void Construct(IHealth health,FlashLight flashLight,IDialogueActor dialogueActor)
        {
            base.Construct(health);
            
            _dialogueActor = dialogueActor;
            _flashLight = flashLight;
            _flashLight.OnIntensityChanged += UpdateFlashLightBar;
            _dialogueActor.OnSentenceCleared += ClearDialogueText;
            _dialogueActor.OnSentenceTyping += TypeDialogueText;
        }

        private void ClearDialogueText() 
            => _dialogueText.text = "";

        

        private void TypeDialogueText(char letter) 
            => _dialogueText.text += letter;

        private void UpdateFlashLightBar() 
            => _flashLightBar.SetValue(_flashLight.CurrentIntensity,_flashLight.MaxIntensity);
    }
}