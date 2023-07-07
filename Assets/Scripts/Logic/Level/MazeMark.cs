using System.Collections.Generic;
using Logic.Player;
using UnityEngine;

namespace Logic.Level
{

    public class MazeMark : MonoBehaviour, IInteractable
    {
        public bool IsLastMark => _isLastMark;
        
        [SerializeField] private bool _isLastMark;
        [SerializeField] private MazeMark _nextMark;
        [SerializeField] private GameObject _objectToDisable;
        [SerializeField] private GameObject _objectToEnable;
        
        [SerializeField] private List<Collider> _collidersToAppear;
        [SerializeField] private List<Collider> _collidersToDisappear;

        public void Interact(Transform interactor) 
            => HandleInteract();

        public string GetInteractText() 
            => string.Empty;

        private void EnableMark() 
            => _objectToEnable.SetActive(true);

        private void DisableObjects()
        {
            foreach (Collider collider in _collidersToDisappear) 
                collider.enabled = false;

            _objectToDisable.SetActive(false);
        }

        private void EnableObjects()
        {
            foreach (Collider collider in _collidersToAppear) 
                collider.enabled = true;
        }

        private void HandleInteract()
        {
            EnableObjects();

            if (_nextMark._isLastMark)
                return;

            _nextMark.EnableMark();
            
            DisableObjects();
        }
    }
}