﻿using Infrastructure.Services.Input;
using Logic.Weapons;
using UnityEngine;
using Zenject;

namespace Logic.Player
{
    public class HeroAttack : MonoBehaviour
    {
        private IInputService _input;
        private IWeapon _currentWeapon;

        [Inject]
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