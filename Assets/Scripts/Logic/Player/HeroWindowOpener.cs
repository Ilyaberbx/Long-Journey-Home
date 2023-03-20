using Infrastructure.Interfaces;
using UI.Services.Window;
using UnityEngine;

namespace Logic.Player
{
    public class HeroWindowOpener : MonoBehaviour
    {
        [SerializeField] private HeroMover _mover;
        [SerializeField] private HeroLook _look;
        [SerializeField] private HeroInteractor _interactor;
        [SerializeField] private HeroAttack _attack;
        
        private IInputService _input;
        private IWindowService _windowService;

        public void Construct(IInputService input, IWindowService windowService)
        {
            _input = input;
            _windowService = windowService;
        }

        private void Update()
        {
            if(_input.IsInventoryButtonPressed())
            {
                ToggleHero(false);
                _windowService.Open(WindowType.Inventory);
            }
        }

        private void ToggleHero(bool value)
        {
            _mover.enabled = value;
            _look.enabled = value;
            _interactor.enabled = value;
            _attack.enabled = value;
        }
    }
}