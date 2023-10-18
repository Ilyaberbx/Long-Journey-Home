using System;
using System.Collections.Generic;
using System.Linq;
using Logic.Gravity;
using Sound.SoundSystem.Operators;
using UnityEngine;

namespace Sound.SoundSystem
{
    public class SoundOperations : MonoBehaviour, ISoundOperations
    {
        private Dictionary<Type, ISoundOperator> _soundOperatorsMap;
        private Dictionary<Type, ISoundOperatorHandleSurface> _soundSurfaceOperatorsMap;
        
        private void Awake() 
            => Initialize();
        
        private void Initialize()
        {
            _soundOperatorsMap = GetComponents<ISoundOperator>()
                .ToDictionary(key => key.GetType(), value => value);
            _soundSurfaceOperatorsMap =  GetComponents<ISoundOperatorHandleSurface>()
                .ToDictionary(key => key.GetType(), value => value);
        }

        public void PlaySound<T>() where T : ISoundOperator
        {
            if (_soundOperatorsMap.TryGetValue(typeof(T), out ISoundOperator soundOperator)) 
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
        
        
    }
}