using System.Threading.Tasks;
using Infrastructure.Services.AssetManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Sound.SoundSystem.Wrappers
{
    [System.Serializable]
    public class SingeSoundsWrapper : ISoundsWrapper
    {
        [SerializeField] private AssetReferenceT<AudioClip> _soundReference;
        private AudioClip _soundClip;
        
        public AudioClip GetAudioClip() 
            => _soundClip;
        public async Task Initialize(IAssetProvider assets) 
            => _soundClip = await assets.Load(_soundReference);
    }
}