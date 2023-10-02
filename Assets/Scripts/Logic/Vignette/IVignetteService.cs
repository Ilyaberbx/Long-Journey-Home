namespace Logic.Vignette
{

    public interface IVignetteService
    {
        void PlayDeath();
        void Reset();
        void UpdateFreeze(float currentWarmLevel,float maxWarmLevel);
        void PlayFreeze();
    }
}