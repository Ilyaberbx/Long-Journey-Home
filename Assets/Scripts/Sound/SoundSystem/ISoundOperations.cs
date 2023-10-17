using Logic.Gravity;
using Sound.SoundSystem.Operators;

namespace Sound.SoundSystem
{
    public interface ISoundOperations
    {
        void PlaySound<T>() where T : ISoundOperator;
        void PlaySound<T>(SurfaceType surface) where T : ISoundOperatorHandleSurface;
    }
}