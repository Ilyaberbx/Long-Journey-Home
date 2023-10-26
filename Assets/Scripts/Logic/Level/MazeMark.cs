using System;
using System.Collections.Generic;
using DG.Tweening;
using Logic.Player;
using UnityEngine;

namespace Logic.Level
{
    public class MazeMark : MonoBehaviour, IInteractable
    {
        [SerializeField] private Collider _colliderToEnable;
        [SerializeField] private Collider _colliderToDisable;
        [SerializeField] private float _enabledDuration;
        [SerializeField] private MazeMark _nextMark;
        [SerializeField] private GameObject _selfInteractor;
        [SerializeField] private GameObject _objectToDisable;
        [SerializeField] private GameObject _objectToEnable;

        private IAction[] _actions;

        private void Awake() 
            => _actions = GetComponentsInChildren<IAction>();

        public void Interact(Transform interactor) 
            => HandleInteract();

        public string GetInteractText() 
            => string.Empty;

        private void EnableMark() 
            => _selfInteractor.SetActive(true);

        private void DisableMark()
            => _selfInteractor.SetActive(false);

        private void DisableObjects() 
            => _objectToDisable.SetActive(false);

        private Sequence EnableObjectsSequence()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.AppendCallback(() => _objectToEnable.SetActive(true));
            sequence.AppendInterval(_enabledDuration);
            return sequence;
        }
        
        private void HandleInteract()
        {
            ExecuteActions();
            DisableMark();
            EnableObjectsSequence().OnComplete(() =>
            {
                ToggleColliders();
                DisableObjects();

                _nextMark?.EnableMark();
            });
            
        }

        private void ExecuteActions()
        {
            foreach (IAction action in _actions)
                action.Execute();
        }

        private void ToggleColliders()
        {
            _colliderToEnable.enabled = true;
            _colliderToDisable.enabled = false;
        }
        
    }
}