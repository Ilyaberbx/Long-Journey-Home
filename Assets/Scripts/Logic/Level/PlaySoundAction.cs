using System.Collections;
using Sound.SoundSystem;
using Sound.SoundSystem.Operators;
using UnityEngine;

namespace Logic.Level
{
    public class PlaySoundAction : MonoBehaviour, IAction
    {
        [SerializeField] private float _delay = 0;
        [SerializeField] private SoundOperations _soundOperations;

        public void Execute() 
            => StartCoroutine(PlaySoundRoutine());

        private IEnumerator PlaySoundRoutine()
        {
            yield return new WaitForSeconds(_delay);
            _soundOperations.PlaySound<SingleSoundOperator>();
        }
    }
}