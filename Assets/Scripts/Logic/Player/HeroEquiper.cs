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
        private EquippableItemData _equippedItemData;
        private BaseEquippableItem _currentItem;
        private IGameFactory _gameFactory;

        [Inject]
        public void Construct(IGameFactory gameFactory) 
            => _gameFactory = gameFactory;

        public void SelectEquipment(EquippableItemData equippableItemData)
        {
            if(_equippedItemData?.Id == equippableItemData.Id)
                return;
            
            ClearUp();

            _equippedItemData = equippableItemData;

            BaseEquippableItem equipment = _gameFactory.CreateEquippableItem(_equippedItemData.ItemPrefab, _equipmentPoint.position,_container);

            _currentItem = equipment;
            _currentItem.transform.localScale = Vector3.zero;
            _currentItem.Appear();

            if (equipment.TryGetComponent(out IWeapon weapon))
                _attack.SetWeapon(weapon);

            if (equipment.TryGetComponent(out IFlashLight flashLight)) 
                flashLight.Construct(_light);
        }

        private void ClearUp()
        {
            _currentItem?.Hide();
            _attack.ClearUp();
        }
    }
}