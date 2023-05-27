using Infrastructure.Services.Input;
using Infrastructure.Services.Settings;
using UnityEngine;
using Zenject;

namespace Logic.Player
{
    public class HeroLook : MonoBehaviour
    {
        private IInputService _input;
        private ISettingsService _settings;

        [Inject]
        public void Construct(IInputService input,ISettingsService settings)
        {
            _input = input;
            _settings = settings;
        }


        private void Update() 
            => Look();

        private void Look() 
            => transform.Rotate(HorizontalLook());

        private Vector3 HorizontalLook() 
            => Vector3.up * (_input.MouseX * _settings.SettingsData.Mouse.Sensitivity);
    }
}