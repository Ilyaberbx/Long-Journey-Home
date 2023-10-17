using Sound.SoundSystem.Wrappers;
using UnityEngine;

namespace Sound.SoundSystem.Operators
{
    public class SingleSoundOperator : SoundOperator<SingeSoundsWrapper>
    {
        public override void PlaySound()
        {
            AudioClip clip = _wrapper.GetAudioClip();
            _source.PlayOneShot(clip);
        }
    }
}