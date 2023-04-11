using System;

namespace Logic.Player
{
    public interface IHeroLight
    {
        float CurrentIntensity { get; set; }
        float MaxIntensity { get; }
        event Action OnIntensityChanged;
    }
}