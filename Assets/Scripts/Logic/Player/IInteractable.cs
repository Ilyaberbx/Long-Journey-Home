using UnityEngine;

namespace Logic.Player
{
    public interface IInteractable
    {
        void Interact(Transform interactorTransform);
        string GetInteractText();
    }
}