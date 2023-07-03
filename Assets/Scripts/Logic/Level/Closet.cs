using System.Collections.Generic;
using DG.Tweening;
using Extensions;
using Logic.Player;
using UnityEngine;

namespace Logic.Level
{
    public class Closet : MonoBehaviour, IInteractable
    {
        [SerializeField] private List<ClosetDoorData> _doors;
        [SerializeField] private string _openedText;
        [SerializeField] private string _closedText;

        private bool _isOpened;
        private Sequence _sequence;

        private void Start()
            => _sequence = DOTween.Sequence();

        public void Interact(Transform interactorTransform)
        {
            if (_isOpened)
                Close();
            else
                Open();
        }

        public string GetInteractText()
            => _isOpened ? _openedText : _closedText;

        private void Close()
        {
            _isOpened = false;
            
            foreach (ClosetDoorData door in _doors)
                _sequence.Append(CloseDoor(door));
        }

        private void Open()
        {
            _isOpened = true;
            
            foreach (ClosetDoorData door in _doors)
                _sequence.Append(OpenDoor(door));
        }

        private Tween CloseDoor(ClosetDoorData door)
            => door.Transform.DOLocalRotate(Vector3.zero.AddY(door.CloseAngle), 0.6f);

        private Tween OpenDoor(ClosetDoorData door)
            => door.Transform.DOLocalRotate(Vector3.zero.AddY(door.OpenAngle), 0.6f);
    }
}