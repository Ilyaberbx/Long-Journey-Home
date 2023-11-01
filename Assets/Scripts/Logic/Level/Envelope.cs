using System.Collections.Generic;
using Data;
using Extensions;
using Logic.Player;
using UnityEngine;

namespace Logic.Level
{
    public class Envelope : MonoBehaviour,IInteractable
    {
        [SerializeField] private EnvelopeData _envelopeData;
        [SerializeField] private string _interactText;

        private IAction[] _actions;
        private List<IStoppableAction> _stoppableActions;

        private void Awake()
        {
            _actions = GetComponentsInChildren<IAction>();
            _stoppableActions = new List<IStoppableAction>();
            CollectStoppable();
        }

        private void CollectStoppable()
        {
            foreach (IAction action in _actions)
                if (action is IStoppableAction stoppableAction)
                    _stoppableActions.Add(stoppableAction);
        }

        public void Interact(Transform interactor)
        {
            IEnvelopeOpenHandler handler = interactor.GetComponent<IEnvelopeOpenHandler>();
            handler.OpenEnvelopeWindow(_envelopeData);
            StopPreviousActions();
            ExecuteActions();
        }

        private void StopPreviousActions()
        {
            foreach (IStoppableAction action in _stoppableActions)
                action.Stop();
        }

        private void ExecuteActions() 
            => _actions.ExecuteAll();

        public string GetInteractText() 
            => _interactText;
    }
}