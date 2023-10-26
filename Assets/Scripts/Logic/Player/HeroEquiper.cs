using System;
using Infrastructure.Services.Factories;
using Logic.Common;
using Logic.Inventory.Item;
using Logic.Weapons;
using UnityEngine;
using Zenject;

namespace Logic.Player
{
    public class HeroEquiper : MonoBehaviour, ICutSceneHandler
    {
        public Transform EquipmentContainer => _equipmentContainer;
        [SerializeField] private HeroLight _light;
        [SerializeField] private HeroAttack _attack;
        [SerializeField] private Transform _equipmentContainer;
        [SerializeField] private Transform _equipmentPoint;
        private EquippableItemData _currentItemData;
        private BaseEquippableItem _currentItem;
        private IGameFactory _gameFactory;

        [Inject]
        public void Construct(IGameFactory gameFactory) 
            => _gameFactory = gameFactory;

        public void SelectEquipment(EquippableItemData item)
        {
            if (!CanEquip(item)) return;

            HideEquipment();
            Equip(item);
        }


        private void Equip(EquippableItemData item)
        {
            _currentItemData = item;

            BaseEquippableItem equipment =
                _gameFactory.CreateEquippableItem(_currentItemData.ItemPrefab, _equipmentPoint.position,
                    _equipmentContainer);

            _currentItem = equipment;
            
            if (_currentItem.TryGetComponent(out IWeapon weapon))
                _attack.SetWeapon(weapon);

            if (_currentItem.TryGetComponent(out IFlashLight flashLight))
                flashLight.Initialize(_light);
            
            _currentItem.transform.localScale = Vector3.zero;
            _currentItem.Appear();
        }
        
        private bool CanEquip(EquippableItemData item)
            => _currentItemData?.Id != item.Id;

        public void ToggleEquipment(bool value)
        {
            if (_currentItemData == null)
                return;

            if (value)
                Equip(_currentItemData);
            else
                HideEquipment();
        }

        public void HideEquipment()
        {
            if (_currentItem == null)
                return;
            
            _currentItem.Hide();
            _attack.ClearUp();
            _currentItem = null;
        }

        public void ClearUp()
        {
            HideEquipment();
            _currentItemData = null;
        }

        public void HandleCutScene(bool isInCutScene)
        {
            if (_currentItem != null && _currentItem.TryGetComponent(out ShakingAlongMove shakingAlongMove)) 
                shakingAlongMove.enabled = !isInCutScene;
        }
        
    }
}