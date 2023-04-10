using Data;
using Infrastructure.Interfaces;
using Logic.Inventory;
using Logic.Weapons;
using UnityEngine;

namespace Logic.Player
{
    public class HeroEquiper : MonoBehaviour
    {
        [SerializeField] private HeroLight _light;
        [SerializeField] private HeroAttack _attack;
        [SerializeField] private Transform _container;
        [SerializeField] private Transform _equipmentPoint;
        private PlayerProgress _progress;

        public void SelectEquipment(EquippableItemData equippableItemData)
        {
            ClearUp();

            equippableItemData.OnDrop += ClearUp;
            
            var equipment =
                Instantiate(equippableItemData.ItemPrefab, _equipmentPoint.position, Quaternion.identity, _container);

            equipment.transform.localScale = Vector3.zero;
            equipment.Appear();

            if (equipment.TryGetComponent<IWeapon>(out var weapon))
                _attack.SetWeapon(weapon);

            if (equipment.TryGetComponent<IFlashLight>(out var flashLight)) 
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