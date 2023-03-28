using System;

namespace Data
{
    [Serializable]
    public class FlashLightState
    {
        public float MaxLightIntensity;
        public float CurrentLightIntensity;

        public void Reset()
            => CurrentLightIntensity = MaxLightIntensity;
    }
}