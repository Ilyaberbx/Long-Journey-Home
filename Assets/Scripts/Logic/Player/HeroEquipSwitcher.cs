using Infrastructure.Interfaces;
using Logic.Weapons;
using UnityEngine;

namespace Logic.Player
{
    public class HeroEquipSwitcher : MonoBehaviour
    {
        [SerializeField] private HeroAttack _attack;
        [SerializeField] private float _switchCoolDown;

        private IEquippable[] _equippableObjects;
        private IEquippable _currentEquipObject;
        private IInputService _input;
        private float _timeSinceLastSwitch;

        public void Construct(IInputService input)
            => _input = input;

        private void Start()
        {
            UpdateEquippableObjects();
            SelectEquipment(1);
        }

        private void Update()
        {
            _timeSinceLastSwitch += Time.deltaTime;

            if (_input.IsSwitchButtonPressed(out int index) && IsCoolDowned()) 
                SelectEquipment(index);
        }

        private bool IsCoolDowned() 
            => _timeSinceLastSwitch >= _switchCoolDown;

        private void SelectEquipment(int index)
        {
            _timeSinceLastSwitch = 0;
            _attack.ClearUp();

            for (int i = 0; i < _equippableObjects.Length; i++)
            {
                if (!IsRightEquipment(i, index))
                {
                    _equippableObjects[i].GetTransform().gameObject.SetActive(false);
                    continue;
                }

                _currentEquipObject = _equippableObjects[i];
                _currentEquipObject.GetTransform().gameObject.SetActive(true);
            }

            if (IsWeapon(out IWeapon weapon))
                _attack.SetWeapon(weapon);
        }

        private bool IsWeapon(out IWeapon weapon) 
            => _currentEquipObject.GetTransform().TryGetComponent(out weapon);

        private bool IsRightEquipment(int i, int index)
            => i == index - 1;

        private void UpdateEquippableObjects()
            => _equippableObjects = GetComponentsInChildren<IEquippable>();
    }
}