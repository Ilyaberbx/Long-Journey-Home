using System.Collections.Generic;
using System.Threading.Tasks;
using Extensions;
using Infrastructure.Services.AssetManagement;
using Logic.Gravity;
using Sound.Player;
using UnityEngine;

namespace Sound.SoundSystem.Wrappers.SurfaceHandle
{
    [System.Serializable]
    public class RandomSurfaceSoundWrapper : ISoundsWrapperHandleSurface
    {
        [SerializeField] private SoundSurfaceData[] _soundBySurfaceDatas;
        private Dictionary<SurfaceType, AudioClip[]> _soundsMap;

        public async Task Initialize(IAssetProvider assets)
        {
            _soundsMap = new Dictionary<SurfaceType, AudioClip[]>();
            
            foreach (SoundSurfaceData soundSurfaceData in _soundBySurfaceDatas)
            {
                AudioClip[] clips = await assets.Load(soundSurfaceData.Clips);
                _soundsMap.Add(soundSurfaceData.Surface, clips);
            }
        }

        public AudioClip GetAudioClip(SurfaceType surfaceType)
        {
            _soundsMap.TryGetValue(surfaceType, out AudioClip[] value);
            return value.Random();
        }
    }
}