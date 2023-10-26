using Sound.SoundSystem.Wrappers;
using UnityEngine;

namespace Sound.SoundSystem.Operators
{
    public class LoopSoundOperator : SoundOperator<SingeSoundsWrapper>
    {
        [SerializeField] private float _sourceVolume = 1f;

        public override void PlaySound()
        {
            AudioClip clip = _wrapper.GetAudioClip();
            _source.loop = true;
            _source.volume = _sourceVolume;
            _source.clip = clip;
            _source.Play();
        }
    }
}