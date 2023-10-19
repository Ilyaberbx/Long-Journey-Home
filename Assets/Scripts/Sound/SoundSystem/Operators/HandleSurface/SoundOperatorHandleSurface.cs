using System.Threading.Tasks;
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
        protected AudioSource _source;
        private IAssetProvider _assetProvider;

        [Inject]
        private async void Construct(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            await InitializeWrapper(assetProvider);
        }

        private Task InitializeWrapper(IAssetProvider assetProvider) 
            => _wrapper.Initialize(assetProvider);

        public abstract void PlaySound(SurfaceType surfaceType);
        public void Initialize(AudioSource source) 
            => _source = source;
    }
}