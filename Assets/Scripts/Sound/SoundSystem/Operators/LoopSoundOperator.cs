using Sound.SoundSystem.Wrappers;
using UnityEngine;

namespace Sound.SoundSystem.Operators
{
    public class LoopSoundOperator : SoundOperator<SingeSoundsWrapper>
    {

        public override void PlaySound()
        {
            AudioClip clip = _wrapper.GetAudioClip();
            _source.loop = true;
            _source.clip = clip;
            _source.Play();
        }
    }
}