using System;

namespace Logic.Weapons
{
    public interface IAmmoUsable
    {
        public event Action OnAmmoChanged;
        public event Action OnDispose;
        public int CurrentAmmo { get; }
        public int MaxAmmo { get; }
    }
}