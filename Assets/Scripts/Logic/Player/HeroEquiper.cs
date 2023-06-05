using Infrastructure.Services.Factories;
using Logic.Inventory.Item;
using Logic.Weapons;
using UnityEngine;
using Zenject;

namespace Logic.Player
{
    public class HeroEquiper : MonoBehaviour
    {
        [SerializeField] private HeroLight _light;
        [SerializeField] private HeroAttack _attack;
        [SerializeField] private Transform _container;
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

            ClearUp();
            Equip(item);
        }
        

        private void Equip(EquippableItemData item)
        {
            _currentItemData = item;

            BaseEquippableItem equipment =
                _gameFactory.CreateEquippableItem(_currentItemData.ItemPrefab, _equipmentPoint.position, _container);

            _currentItem = equipment;
            _currentItem.transform.localScale = Vector3.zero;
            _currentItem.Appear();

            if (equipment.TryGetComponent(out IWeapon weapon))
                _attack.SetWeapon(weapon);

            if (equipment.TryGetComponent(out IFlashLight flashLight))
                flashLight.Init(_light);
        }

        private bool CanEquip(EquippableItemData item) 
            => _currentItemData?.Id != item.Id;

        public void ToggleEquipment(bool value)
        {
            if(_currentItemData == null)
                return;
            
            if (value)
                Equip(_currentItemData);
            else
                _currentItem.Hide();
        }
        
        private void ClearUp()
        {
            _currentItem?.Hide();
            _attack.ClearUp();
        }
    }
}