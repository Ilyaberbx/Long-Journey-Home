using Sound.SoundSystem.Wrappers;
using UnityEngine;

namespace Sound.SoundSystem.Operators
{
    public class RandomSoundOperator : SoundOperator<RandomSoundsWrapper>
    {
        public override void PlaySound()
        {
           AudioClip sound = _wrapper.GetAudioClip();
           _source.PlayOneShot(sound);
        }
    }
}