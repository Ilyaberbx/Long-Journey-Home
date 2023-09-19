using Infrastructure.Interfaces;
using Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace Logic.Player
{
    public class HeroInteractor : MonoBehaviour, IInteractor
    {
        [SerializeField] private float _interactRange;

        private IInputService _input;
        private IInteractable _interactable;
        private bool _hintShowed;

        [Inject]
        public void Construct(IInputService input) 
            => _input = input;

        private void Update()
        {
            if(!_input.IsInteractButtonPressed()) return;
            
            _interactable = GetInteractableObject();
            
            _interactable?.Interact(transform);
        }

        public IInteractable GetInteractableObject()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _interactRange);

            foreach (Collider collider in colliders)
                if (collider.TryGetComponent(out IInteractable interactable))
                    return interactable;

            return null;
        }
    }
}