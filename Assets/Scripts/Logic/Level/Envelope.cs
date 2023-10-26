using System;
using Data;
using Logic.Player;
using UnityEngine;

namespace Logic.Level
{
    public class Envelope : MonoBehaviour,IInteractable
    {
        [SerializeField] private EnvelopeData _envelopeData;
        [SerializeField] private string _interactText;

        private IAction[] _actions;

        private void Awake() 
            => _actions = GetComponentsInChildren<IAction>();

        public void Interact(Transform interactor)
        {
            IEnvelopeOpenHandler handler = interactor.GetComponent<IEnvelopeOpenHandler>();
            handler.OpenEnvelopeWindow(_envelopeData);
            ExecuteActions();
        }

        private void ExecuteActions()
        {
            foreach (IAction action in _actions) 
                action.Execute();
        }

        public string GetInteractText() 
            => _interactText;
    }
}