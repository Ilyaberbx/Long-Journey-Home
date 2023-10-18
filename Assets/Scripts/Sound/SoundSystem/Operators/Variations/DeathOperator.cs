namespace Sound.SoundSystem.Operators.Variations
{
    public class DeathOperator : SingleSoundOperator
    {
        public override void PlaySound()
        {
            _source.Stop();
            base.PlaySound();
        }
    }
}