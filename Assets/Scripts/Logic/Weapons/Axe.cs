﻿using DG.Tweening;
using Logic.Player;
using UnityEngine;

namespace Logic.Weapons
{
    public class Axe : MonoBehaviour, IWeapon, IEquippable
    {
        private const string HittableLayerName = "Hittable";

        [SerializeField] private CheckPoint _attackPoint;
        [SerializeField] private int _damage;
        [SerializeField] private float _attackRadius;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private GameObject _bloodFx;
        [SerializeField] private float _offset;

        private IWeaponAnimator _animator;
        private bool _isAttacking;

        private int _layerMask;
        private Collider[] _hits = new Collider[3];
        private Vector3 _cachedScale;


        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer(HittableLayerName);
            _animator = GetComponent<IWeaponAnimator>();
            _cachedScale = transform.localScale;
        }

        public void PerformAttack()
        {
            if (_isAttacking) return;

            _animator.PlayAttack();
            _animator.SetAnimatorSpeed(_attackSpeed);
            _isAttacking = true;
        }

        public Transform GetTransform()
            => transform;

        public void Appear()
        {
            _isAttacking = false;
            transform.localScale = Vector3.zero;
            transform.DOScale(_cachedScale, 0.2f);
        }

        private void OnAttack()
        {
            for (int i = 0; i < Hit(); i++)
                ProcessAttack(i);

            _animator.SetAnimatorSpeed(1);
            _isAttacking = false;
        }

        private void ProcessAttack(int index)
        {
            _hits[index].transform.parent.GetComponent<IHealth>().TakeDamage(_damage);
            ShowFx(index);
        }

        private void ShowFx(int index)
        {
            Instantiate(_bloodFx.gameObject, _hits[index].attachedRigidbody.position + Vector3.up * _offset,
                Quaternion.identity);
        }

        private int Hit()
            => Physics.OverlapSphereNonAlloc(_attackPoint.Position, _attackRadius, _hits, _layerMask);
    }
}