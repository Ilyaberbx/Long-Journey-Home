using System;
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
        [SerializeField] private Bar _freezeBar;
        [SerializeField] private TextMeshProUGUI _dialogueText;
        [SerializeField] private InteractHint _interactHint;

        private FlashLight _flashLight;
        private IDialogueActor _dialogueActor;
        private IFreeze _freeze;
        private IInteractor _interactor;

        public void Construct(IHealth health,FlashLight flashLight,IDialogueActor dialogueActor,IFreeze freeze,IInteractor interactor)
        {
            base.Construct(health);

            _freeze = freeze;
            _interactor = interactor;
            _dialogueActor = dialogueActor;
            _flashLight = flashLight;
            _flashLight.OnIntensityChanged += UpdateFlashLightBar;
            _dialogueActor.OnSentenceCleared += ClearDialogueText;
            _dialogueActor.OnSentenceTyping += TypeDialogueText;
            _freeze.OnFreezeChanged += UpdateFreezeBar;
        }

        private void Update() 
            => ShowHint(_interactor.GetInteractableObject());

        private void ShowHint(IInteractable interactable)
        {
            if (interactable != null)
                _interactHint.Show(interactable);
            else
                _interactHint.Hide();
        }

        private void UpdateFreezeBar()
            => _freezeBar.SetValue(_freeze.CurrentFreeze,_freeze.MaxFreeze);

        private void ClearDialogueText() 
            => _dialogueText.text = "";

        

        private void TypeDialogueText(char letter) 
            => _dialogueText.text += letter;

        private void UpdateFlashLightBar() 
            => _flashLightBar.SetValue(_flashLight.CurrentIntensity,_flashLight.MaxIntensity);
    }
}