using Logic.Gravity;
using UnityEngine;

namespace Sound.SoundSystem.Wrappers.SurfaceHandle
{
    public interface ISoundsWrapperHandleSurface : IWrapper
    {
        AudioClip GetAudioClip(SurfaceType surfaceType);
    }
}