using Infrastructure.Interfaces;
using Infrastructure.Services.Factories;
using Logic.Inventory;
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
        private IGameFactory _gameFactory;

        [Inject]
        public void Construct(IGameFactory gameFactory) 
            => _gameFactory = gameFactory;

        public void SelectEquipment(EquippableItemData equippableItemData)
        {
            ClearUp();

            _equippedItemData = equippableItemData;
            _equippedItemData.OnDrop += ClearUp;

            BaseEquippableItem equipment = _gameFactory.CreateEquippableItem(_equippedItemData.ItemPrefab, _equipmentPoint.position,_container);

            equipment.transform.localScale = Vector3.zero;
            equipment.Appear();

            if (equipment.TryGetComponent(out IWeapon weapon))
                _attack.SetWeapon(weapon);

            if (equipment.TryGetComponent(out IFlashLight flashLight)) 
                flashLight.Construct(_light);
        }

        private void ClearUp()
        {
            foreach (Transform child in _container)
                Destroy(child.gameObject);

            _attack.ClearUp();
        }
    }
}