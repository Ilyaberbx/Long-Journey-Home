using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic.Gravity;
using Sound.SoundSystem.Operators;
using UnityEngine;

namespace Sound.SoundSystem
{
    public class SoundOperations : MonoBehaviour, ISoundOperations
    {
        [SerializeField] private AudioSource _audioSource;
        private Dictionary<Type, INoArgumentSoundOperator> _soundOperatorsMap;
        private Dictionary<Type, ISoundOperatorHandleSurface> _soundSurfaceOperatorsMap;

        private async void Awake()
            => await Initialize();

        private Task Initialize()
        {
            INoArgumentSoundOperator[] simpleOperators = GetComponents<INoArgumentSoundOperator>();
            ISoundOperatorHandleSurface[] handleSurfaceOperators = GetComponents<ISoundOperatorHandleSurface>();

            InitializeOperators(handleSurfaceOperators);
            InitializeOperators(simpleOperators);

            _soundSurfaceOperatorsMap = handleSurfaceOperators.ToDictionary(key => key.GetType(), value => value);
            _soundOperatorsMap = simpleOperators.ToDictionary(key => key.GetType(), value => value);
            return Task.CompletedTask;
        }

        public void PlaySound<T>() where T : INoArgumentSoundOperator
        {
            if (_soundOperatorsMap.TryGetValue(typeof(T), out INoArgumentSoundOperator soundOperator))
                soundOperator.PlaySound();
            else
                Debug.Log("No sound operators found");
        }

        public void PlaySound<T>(SurfaceType surface) where T : ISoundOperatorHandleSurface
        {
            if (_soundSurfaceOperatorsMap.TryGetValue(typeof(T), out ISoundOperatorHandleSurface soundOperator))
                soundOperator.PlaySound(surface);
            else
                Debug.Log("No sound operators found");
        }

        public void Stop()
        {
            _audioSource.clip = null;
            _audioSource.Stop();
        }

        public void Pause() 
            => _audioSource.Pause();

        public void Resume() 
            => _audioSource.Play();

        private void InitializeOperators(ISoundOperator[] operators)
        {
            foreach (ISoundOperator soundOperator in operators) 
                soundOperator.Initialize(_audioSource);
        }
    }
}