using UnityEngine;

namespace Sound.SoundSystem.Wrappers
{
    public interface ISoundsWrapper : IWrapper
    {
        AudioClip GetAudioClip();
    }
}