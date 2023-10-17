using System.Threading.Tasks;
using Extensions;
using Infrastructure.Services.AssetManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Sound.SoundSystem.Wrappers
{
    [System.Serializable]
    public class RandomSoundsWrapper : ISoundsWrapper
    {
        [SerializeField] private AssetReferenceT<AudioClip>[] _soundReferences;
        
        private AudioClip[] _soundClips;
        
        public AudioClip GetAudioClip() 
            => _soundClips.Random();

        public async Task Initialize(IAssetProvider assets) 
            => _soundClips = await assets.Load(_soundReferences);
        
    }
}