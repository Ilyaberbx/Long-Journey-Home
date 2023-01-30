using System;

namespace Interfaces
{
    public interface IHealth
    {
        event Action OnHealthChanged;
        int CurrentHealth { get; set; }
        int MaxHp { get; set; }
        void TakeDamage(int damage);
    }
}