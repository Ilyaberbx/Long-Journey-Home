using System;
using Data;
using DG.Tweening;
using Infrastructure.Services.Input;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using Logic.Inventory.Item;
using Logic.Player;
using Sound.SoundSystem;
using Sound.SoundSystem.Operators.Variations;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Logic.Weapons
{
    public class ShootGun : BaseEquippableItem, IWeapon, IAmmoUsable,ISavedProgressWriter
    {
        public event Action OnAmmoChanged;
        public event Action OnDispose;
        int IAmmoUsable.CurrentAmmo => _ammoInMagazine;
        public int MaxAmmo => _magazineCapacity;
        [SerializeField] private SoundOperations _soundOperations;
        [SerializeField] private ParticleSystem _hitMarkFx;
        [SerializeField] private ParticleSystem _smokeFx;
        [SerializeField] private ParticleSystem _sparksFx;
        [SerializeField] private GameObject _bloodFx;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private AmmoItemData _ammoItemData;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private float _reloadSpeed;
        [SerializeField] private float _scatter;
        [SerializeField] private int _magazineCapacity;
        [SerializeField] private int _shotsPerBullet;

        private IReloadableWeaponAnimator _animator;
        private IInputService _input;
        private IPersistentProgressService _progressService;
        private InventoryData InventoryData => _progressService.Progress.InventoryData;
        private bool _isAttacking;
        private bool _isReloading;
        private int _ammoInMagazine;

        private Transform _cachedTransform;
        private Vector3 _cachedScale;
        private Vector3 _randomRayDirection;


        [Inject]
        public void Construct(IPersistentProgressService progressService, IInputService input)
        {
            _input = input;
            _progressService = progressService;
        }

        private void Awake()
        {
            _animator = GetComponent<IReloadableWeaponAnimator>();
            _cachedTransform = transform;
            _cachedScale = _cachedTransform.localScale;
        }

        private void Update()
        {
            if (_input.IsReloadButtonPressed())
                Reload();
        }

        private void OnDestroy()
        {
            PutAmmoBackToInventory();
            OnDispose?.Invoke();
        }

        private void PutAmmoBackToInventory()
        {
            InventoryData.AddItem(_ammoItemData, _ammoInMagazine);
            _ammoInMagazine = 0;
        }

        public override void Hide() 
            => Destroy(gameObject);

        public override void Appear()
        {
            _isAttacking = false;
            transform.DOScale(_cachedScale, 0.2f);
        }

        public void PerformAttack()
        {
            if (!CanShoot()) return;

            _ammoInMagazine--;
            _animator.PlayAttack(0);
            _animator.SetAnimatorSpeed(_attackSpeed);
            _isAttacking = true;
        }

        private void ShowFx()
        {
            Vector3 position = _shootPoint.position;
            Instantiate(_smokeFx, position, Quaternion.identity);
            Instantiate(_sparksFx, position, Quaternion.LookRotation(-_cachedTransform.right),
                _cachedTransform);
        }


        private bool TryWithDrawAmmo()
            => InventoryData.TryRemoveItemById(_ammoItemData.Id, 1);


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
            => InventoryData.HasItemById(_ammoItemData.Id) && !_isReloading && CalculateAmmoReminder() > 0;

        private void OnReload()
        {
            int noLoadedAmmo = CalculateAmmoReminder();

            for (int i = 0; i < noLoadedAmmo; i++)
            {
                if (TryWithDrawAmmo())
                {
                    PlayReloadSound();
                    _ammoInMagazine++;
                }
                else
                    break;
            }
            
            InformAmmoChanged();

            _isReloading = false;
        }

        private void OnAttack()
        {
            PlayAttackSound();
            Hit();
            ShowFx();
            InformAmmoChanged();
            _animator.SetAnimatorSpeed(1);
            _isAttacking = false;
        }

        private void PlayAttackSound() 
            => _soundOperations.PlaySound<AttackOperator>();
        
        private void PlayReloadSound() 
            => _soundOperations.PlaySound<ReloadOperator>();


        private Vector3 CalculateCastDirection()
            => (-_cachedTransform.right + _randomRayDirection) * 150f;

        private float CalculateScatter()
            => Random.Range(-_scatter, _scatter);

        private void Hit()
        {
            RaycastHit hit = new RaycastHit();
            for (int i = 0; i < _shotsPerBullet; i++)
            {
                _randomRayDirection = new Vector3(CalculateScatter(), CalculateScatter(), 0);

                if (!IsHit(out hit)) continue;

                if (IsDamagable(hit, out var health))
                {
                    _ammoItemData.ApplyHit(health);
                    ShowBloodFx(hit);
                }
                else
                    ShowHitMark(hit);
            }
        }

        private void ShowBloodFx(RaycastHit hit)
            => Instantiate(_bloodFx, hit.point - Vector3.up * 5, Quaternion.identity);

        private void ShowHitMark(RaycastHit hit)
            => Instantiate(_hitMarkFx, hit.point + hit.normal * .01f,
                Quaternion.FromToRotation(Vector3.forward, hit.normal));

        private bool IsDamagable(RaycastHit hit, out IHealth health)
            => hit.transform.gameObject.TryGetComponent(out health);

        private bool IsHit(out RaycastHit hit)
            => Physics.Raycast(_cachedTransform.position, CalculateCastDirection(), out hit);

        private void InformAmmoChanged()
            => OnAmmoChanged?.Invoke();

        private int CalculateAmmoReminder()
            => _magazineCapacity - _ammoInMagazine;

        public void LoadProgress(PlayerProgress progress)
        { }

        public void UpdateProgress(PlayerProgress progress) 
            => PutAmmoBackToInventory();
    }
}