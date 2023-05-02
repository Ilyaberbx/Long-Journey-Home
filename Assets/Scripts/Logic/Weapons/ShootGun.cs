using Data;
using DG.Tweening;
using Infrastructure.Services.PersistentProgress;
using Logic.Inventory.Item;
using UnityEngine;
using Zenject;

namespace Logic.Weapons
{
    public class ShootGun : BaseEquippableItem, IWeapon
    {
        [SerializeField] private ItemData _ammoItemData;
        [SerializeField] private int _damage;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private float _reloadSpeed;

        private IReloadableWeaponAnimator _animator;
        private InventoryData _inventoryData;
        private bool _isAttacking;
        private bool _isReloading;

        private Vector3 _cachedScale;

        [Inject]
        public void Construct(IPersistentProgressService progressService) 
            => _inventoryData = progressService.PlayerProgress.InventoryData;

        private void Awake()
        {
            _animator = GetComponent<IReloadableWeaponAnimator>();
            _cachedScale = transform.localScale;
        }

        public override void Appear()
        {
            _isAttacking = false;
            transform.localScale = Vector3.zero;
            transform.DOScale(_cachedScale, 0.2f);
        }

        public void PerformAttack()
        {
            if (!CanShoot()) return;
            
           if(!_inventoryData.TryWithDrawItem(_ammoItemData))
               return;
           
           Debug.Log("ShootGunAttack");
            _animator.PlayAttack();
            _animator.SetAnimatorSpeed(_attackSpeed);
            _isAttacking = true;
        }
        

        private bool CanShoot()
            => !_isAttacking && !_isReloading;

        

        private void Reload()
        {
            _animator.PlayReload();
            _animator.SetAnimatorSpeed(_reloadSpeed);
            _isReloading = true;
        }

        private void OnAttack()
        {
            _animator.SetAnimatorSpeed(1);
            _isAttacking = false;
        }

        private void OnReload()
            => _isReloading = false;
        
    }
}