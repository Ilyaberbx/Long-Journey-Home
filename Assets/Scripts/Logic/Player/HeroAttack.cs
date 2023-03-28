using Infrastructure.Interfaces;
using Logic.Weapons;
using UnityEngine;

namespace Logic.Player
{
    public class HeroAttack : MonoBehaviour
    {
        private IInputService _input;
        private IWeapon _currentWeapon;

        public void Construct(IInputService inputService)
            => _input = inputService;

        private void Update()
        {
            if (!_input.IsAttackButtonPressed()) return;
            
            _currentWeapon?.PerformAttack();
        }

        public void SetWeapon(IWeapon weapon) 
            => _currentWeapon = weapon;

        public void ClearUp()
            => _currentWeapon = null;
    }
}