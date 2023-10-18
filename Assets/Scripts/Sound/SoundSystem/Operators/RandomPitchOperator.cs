using UnityEngine;

namespace Sound.SoundSystem.Operators
{
    public class RandomPitchOperator : RandomVolumeOperator
    {
        [SerializeField] private float _minPitch;
        [SerializeField] private float _maxPitch;

        public override void PlaySound()
        {
            _source.pitch = Random.Range(_minPitch, _maxPitch);
            base.PlaySound();
        }
    }
}