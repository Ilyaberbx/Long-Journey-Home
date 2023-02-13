using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class FlashLightState
    {
        public float MaxLightIntensity;
        public float LightIntensity;

        public void Reset() 
            => LightIntensity = MaxLightIntensity;

        public void Add(FlashLightLoot flashLightLoot)
        {
            float addIntensity = MaxLightIntensity * flashLightLoot.Value;
            LightIntensity = CalculateLightIntensity(addIntensity);
        }

        private int CalculateLightIntensity(float addIntensity) 
            => (int)Mathf.Clamp(LightIntensity + addIntensity,0, MaxLightIntensity);
    }
}