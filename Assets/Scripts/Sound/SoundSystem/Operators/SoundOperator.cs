using System.Threading.Tasks;
using Infrastructure.Services.AssetManagement;
using Sound.SoundSystem.Wrappers;
using UnityEngine;
using Zenject;

namespace Sound.SoundSystem.Operators
{
    public abstract class SoundOperator<T> : MonoBehaviour, INoArgumentSoundOperator where T : class, ISoundsWrapper
    {
        [SerializeField] protected T _wrapper;
        protected AudioSource _source;

        [Inject]
        private async Task Construct(IAssetProvider assetProvider) 
            => await InitializeWrapper(assetProvider);

        private Task InitializeWrapper(IAssetProvider assetProvider)
            => _wrapper.Initialize(assetProvider);

        public void Initialize(AudioSource source) 
            => _source = source;

        public abstract void PlaySound();
    }
}