using System;

namespace Logic.Player
{
    public interface IFreezable
    {
        event Action OnFreezeChanged;
        float MaxFreeze { get; }
        float CurrentFreeze { get; set; }
        void DecreaseCurrentWarmLevel(float value);
    }
}