using System.Collections.Generic;
using DG.Tweening;
using Extensions;
using Logic.Level.Sound;
using Logic.Player;
using Sound.SoundSystem;
using UnityEngine;

namespace Logic.Level
{
    public class Closet : MonoBehaviour, IInteractable
    {
        [SerializeField] private SoundOperations _soundOperations;
        [SerializeField] private Collider _closetCollider;
        [SerializeField] private List<ClosetDoorData> _doors;
        [SerializeField] private string _openedText;
        [SerializeField] private string _closedText;

        private bool _isOpened;
        private Sequence _sequence;
        
        public void Interact(Transform interactor)
        {
            if (_isOpened)
                Close();
            else
                Open();
        }

        public string GetInteractText()
            => _isOpened ? _closedText : _openedText;

        private void Close()
        {
            _isOpened = false;
            _sequence = DOTween.Sequence();
            
            foreach (ClosetDoorData door in _doors)
                _sequence.Append(CloseDoor(door));
            
        }

        private void Open()
        {
            _isOpened = true;
            _sequence = DOTween.Sequence();
            _closetCollider.enabled = false;
            _soundOperations.PlaySound<OpenSoundOperator>();
            
            foreach (ClosetDoorData door in _doors)
                _sequence.Append(OpenDoor(door));
        }

        private Tween CloseDoor(ClosetDoorData door)
            => door.Transform.DOLocalRotate(Vector3.zero.AddY(door.CloseAngle), 0.6f);

        private Tween OpenDoor(ClosetDoorData door)
            => door.Transform.DOLocalRotate(Vector3.zero.AddY(door.OpenAngle), 0.6f);
    }
}