using System;
using Infrastructure.Interfaces;
using UI.Services.Window;
using UnityEngine;

namespace Logic.Player
{
    public class HeroWindowOpener : MonoBehaviour
    {
        private IInputService _input;
        private IWindowService _windowService;

        public void Construct(IInputService input,IWindowService windowService)
        {
            _input = input;
            _windowService = windowService;
        }

        private void Update()
        {
            if(!_input.IsInventoryButtonPressed()) return;
            _windowService.Open(WindowType.Inventory);
        }
    }
}