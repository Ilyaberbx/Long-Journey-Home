using System;

namespace Logic.Player
{
    public interface IHealth
    {
        event Action OnHealthChanged;
        int CurrentHealth { get; set; }
        int MaxHp { get; set; }
        void TakeDamage(int damage,bool withAnimation = true);
    }
}