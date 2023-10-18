using UnityEngine;

namespace Sound.SoundSystem.Operators
{

    public class RandomVolumeOperator : RandomSoundOperator
    {
        [SerializeField] private float _minVolume;
        [SerializeField] private float _maxVolume;
        
        public override void PlaySound()
        {
            _source.volume = Random.Range(_minVolume, _maxVolume);
            base.PlaySound();
        }
    }
}