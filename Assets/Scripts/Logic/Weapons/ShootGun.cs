using System;
using Data;
using DG.Tweening;
using Infrastructure.Services.Factories;
using Infrastructure.Services.Input;
using Infrastructure.Services.PersistentProgress;
using Logic.Inventory.Item;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Logic.Weapons
{
    public class ShootGun : BaseEquippableItem, IWeapon, IHudAmmoShowable
    {
        public event Action OnAmmoChanged;
        public event Action OnDispose;
        public int CurrentAmmo => _ammoInMagazine;
        public int MaxAmmo => _magazineCapacity;

        
        [SerializeField] private ParticleSystem _smokeFx;
        [SerializeField] private ParticleSystem _sparksFx;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private AmmoItemData _ammoItemData;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private float _reloadSpeed;
        [SerializeField] private int _magazineCapacity;

        private IReloadableWeaponAnimator _animator;
        private IInputService _input;
        private IPersistentProgressService _progressService;
        private InventoryData _inventoryData => _progressService.PlayerProgress.InventoryData;
        private bool _isAttacking;
        private bool _isReloading;
        private int _ammoInMagazine;

        private Vector3 _cachedScale;
        private IGameFactory _gameFactory;
        private Transform _cachedTransform;


        [Inject]
        public void Construct(IPersistentProgressService progressService, IInputService input)
        {
            _input = input;
            _progressService = progressService;
        }

        private void Awake()
        {
            _animator = GetComponent<IReloadableWeaponAnimator>();
            _cachedScale = transform.localScale;
            _cachedTransform = transform;
        }

        private void Update()
        {
            if (_input.IsReloadButtonPressed())
                Reload();
        }

        private void OnDestroy()
        {
            _inventoryData.AddItem(_ammoItemData, _ammoInMagazine);
            OnDispose?.Invoke();
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

            ShowFx();
            _ammoInMagazine--;
            _animator.PlayAttack();
            _animator.SetAnimatorSpeed(_attackSpeed);
            _isAttacking = true;
        }

        private void ShowFx()
        {
            Instantiate(_smokeFx, _shootPoint.position, Quaternion.identity);
            Instantiate(_sparksFx, _shootPoint.position, Quaternion.LookRotation(-_cachedTransform.right),_cachedTransform);
        }


        private bool TryWithDrawAmmo()
            => _inventoryData.TryRemoveItemById(_ammoItemData.Id, 1);


        private bool CanShoot()
            => !_isAttacking && !_isReloading && _ammoInMagazine > 0;

        private void Reload()
        {
            if (!CanReload()) return;

            _animator.PlayReload();
            _animator.SetAnimatorSpeed(_reloadSpeed);
            _isReloading = true;
        }

        private bool CanReload()
            => _inventoryData.HasItemById(_ammoItemData.Id) && !_isReloading && CalculateAmmoReminder() > 0;

        private void OnReload()
        {
            Debug.Log("On Reloaded");
            int noLoadedAmmo = CalculateAmmoReminder();

            for (int i = 0; i < noLoadedAmmo; i++)
            {
                if (TryWithDrawAmmo())
                    _ammoInMagazine++;
                else
                    break;
            }

            InformAmmoChanged();

            _isReloading = false;
        }

        private void OnAttack()
        {
            Debug.Log("On Attack");
            InformAmmoChanged();
            _animator.SetAnimatorSpeed(1);
            _isAttacking = false;
        }

        private void InformAmmoChanged()
            => OnAmmoChanged?.Invoke();

        private int CalculateAmmoReminder()
            => _magazineCapacity - _ammoInMagazine;
    }
}