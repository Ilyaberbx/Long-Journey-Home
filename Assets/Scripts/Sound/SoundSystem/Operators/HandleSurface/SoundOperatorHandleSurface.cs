using Infrastructure.Services.AssetManagement;
using Logic.Gravity;
using Sound.SoundSystem.Wrappers.SurfaceHandle;
using UnityEngine;
using Zenject;

namespace Sound.SoundSystem.Operators
{
    public abstract class SoundOperatorHandleSurface<T> : MonoBehaviour,ISoundOperatorHandleSurface
        where T : ISoundsWrapperHandleSurface
    {
        [SerializeField] protected T _wrapper;
        [SerializeField] protected AudioSource _source;
        
        [Inject]
        private async void Construct(IAssetProvider assetProvider) 
            => await _wrapper.Initialize(assetProvider);
        
        public abstract void PlaySound(SurfaceType surfaceType);
    }
}