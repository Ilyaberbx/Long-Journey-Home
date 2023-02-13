using System;

namespace Data
{
    [Serializable]
    public class HealthState
    {
        public int CurrentHP;
        public int MaxHP;

        public void ResetHp()
            => CurrentHP = MaxHP;
    }
}