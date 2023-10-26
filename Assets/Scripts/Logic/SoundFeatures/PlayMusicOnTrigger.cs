using Infrastructure.Services.MusicService;
using Logic.Common;
using UnityEngine;
using Zenject;

namespace Logic.SoundFeatures
{
    public class PlayMusicOnTrigger : MonoBehaviour
    {
        [SerializeField] private PlayMusicAction _action;
        [SerializeField] private TriggerObserver _triggerObserver;
        private void Awake()
            => _triggerObserver.OnTriggerEntered += ProcessTrigger;

        private void OnDestroy()
            => _triggerObserver.OnTriggerEntered -= ProcessTrigger;

        private void ProcessTrigger(Collider obj)
        {
            _action.Execute();
            _triggerObserver.Disable();
        }
        
    }
}