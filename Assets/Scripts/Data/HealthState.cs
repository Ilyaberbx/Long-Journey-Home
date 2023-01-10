using System;

namespace ProjectSolitude.Data
{
    [Serializable]
    public class HealthState
    {
        public float CurrentHP;
        public float MaxHP;

        public void ResetHp()
            => CurrentHP = MaxHP;
    }
}