using Data;
using Logic.Player;
using UnityEngine;

namespace Logic.Level
{
    public class Envelope : MonoBehaviour,IInteractable
    {
        [SerializeField] private EnvelopeData _envelopeData;
        [SerializeField] private string _interactText;
        public void Interact(Transform interactor)
        {
            IEnvelopeOpenHandler handler = interactor.GetComponent<IEnvelopeOpenHandler>();
            handler.OpenEnvelopeWindow(_envelopeData);
        }

        public string GetInteractText() 
            => _interactText;
    }
}