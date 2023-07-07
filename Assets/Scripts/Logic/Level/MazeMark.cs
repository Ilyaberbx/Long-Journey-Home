using DG.Tweening;
using Logic.Player;
using UnityEngine;

namespace Logic.Level
{

    public class MazeMark : MonoBehaviour, IInteractable
    {
        [SerializeField] private float _enabledDuration;
        [SerializeField] private bool _isLastMark;
        [SerializeField] private MazeMark _nextMark;
        [SerializeField] private GameObject _selfInteractor;
        [SerializeField] private GameObject _objectToDisable;
        [SerializeField] private GameObject _objectToEnable;

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
            sequence.AppendCallback(() => _objectToDisable.SetActive(false));
            return sequence;
        }

        private void HandleInteract()
        {
            EnableObjectsSequence().OnComplete(() =>
            {
                if (_nextMark._isLastMark)
                    return;

                _nextMark.EnableMark();

                DisableObjects();
                DisableMark();
            });
            
        }
    }
}