using Infrastructure.Services.AssetManagement;
using Sound.SoundSystem.Wrappers;
using UnityEngine;
using Zenject;

namespace Sound.SoundSystem.Operators
{

    public abstract class SoundOperator<T> : MonoBehaviour, ISoundOperator where T : class, ISoundsWrapper
    {
        [SerializeField] protected T _wrapper;
        [SerializeField] protected AudioSource _source;
        
        [Inject]
        private async void Construct(IAssetProvider assetProvider) 
            => await _wrapper.Initialize(assetProvider);
        
        public abstract void PlaySound();
    }
}