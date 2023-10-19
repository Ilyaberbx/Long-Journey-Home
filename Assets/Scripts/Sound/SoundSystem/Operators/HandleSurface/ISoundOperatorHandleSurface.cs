using Logic.Gravity;

namespace Sound.SoundSystem.Operators
{
    public interface ISoundOperatorHandleSurface : ISoundOperator
    {
        void PlaySound(SurfaceType surfaceType);
    }
}