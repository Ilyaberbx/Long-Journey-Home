using Interfaces;
using Logic.Player;
using UnityEngine;

namespace UI
{
    public class PlayerUIActor : UIActor
    {
        [SerializeField] private Bar _flashLightBar;
        private FlashLight _flashLight;

        public void Construct(IHealth health,FlashLight flashLight)
        {
            base.Construct(health);
            _flashLight = flashLight;
            _flashLight.OnIntensityChanged += UpdateFlashLightBar;
        }

        private void UpdateFlashLightBar() 
            => _flashLightBar.SetValue(_flashLight.CurrentIntensity,_flashLight.MaxIntensity);
    }
}