using Logic.Gravity;
using Sound.SoundSystem.Wrappers.SurfaceHandle;
using UnityEngine;

namespace Sound.SoundSystem.Operators
{
    public class RandomSoundOperatorHandleSurface : SoundOperatorHandleSurface<RandomSurfaceSoundWrapper>
    {
        public override void PlaySound(SurfaceType surfaceType)
        {
            AudioClip clip = _wrapper.GetAudioClip(surfaceType);
            _source.PlayOneShot(clip);
        }
    }
}