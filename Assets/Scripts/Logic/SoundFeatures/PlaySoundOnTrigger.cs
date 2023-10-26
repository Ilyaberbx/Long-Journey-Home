using Logic.Common;
using Logic.Level;
using Sound.SoundSystem;
using Sound.SoundSystem.Operators;
using UnityEngine;

namespace Logic.SoundFeatures
{
    public class PlaySoundOnTrigger : MonoBehaviour
    {
        [SerializeField] private bool _disableOnTrigger;
        [SerializeField] private PlaySoundAction _soundAction;
        [SerializeField] private TriggerObserver _triggerObserver;

        private void Awake() 
            => _triggerObserver.OnTriggerEntered += ProcessTrigger;

        private void OnDestroy()
            => _triggerObserver.OnTriggerEntered -= ProcessTrigger;

        private void ProcessTrigger(Collider obj)
        {
            PlaySound();
            
            if(_disableOnTrigger)
                _triggerObserver.Disable();
        }

        private void PlaySound()
            => _soundAction.Execute();
    }
}