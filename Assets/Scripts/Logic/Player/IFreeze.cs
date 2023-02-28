using System;

namespace Logic.Player
{
    public interface IFreeze
    {
        event Action OnFreezeChanged;
        float MaxFreeze { get; }
        float CurrentFreeze { get; set; }
    }
}