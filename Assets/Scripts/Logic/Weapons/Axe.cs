using Data;
using Infrastructure.Interfaces;
using Logic.Player;
using UnityEngine;

namespace Logic.Weapons
{
    public class Axe : MonoBehaviour, IWeapon, ISavedProgressReader, IEquippable
    {
        private const string HittableLayerName = "Hittable";

        public IWeaponAnimator WeaponAnimator => _animator;

        [SerializeField] private CheckPoint _attackPoint;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private ParticleSystem _bloodFx;
        [SerializeField] private float _offset;

        private IWeaponAnimator _animator;
        private bool _isAttacking;

        private int _layerMask;
        private Collider[] _hits = new Collider[3];
        private Stats _stats;

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer(HittableLayerName);
            _animator = GetComponent<IWeaponAnimator>();
        }

        public void PerformAttack()
        {
            if (_isAttacking) return;

            _animator.PlayAttack();
            _animator.SetAnimatorSpeed(_attackSpeed);
            _isAttacking = true;
        }

        public void LoadProgress(PlayerProgress progress)
            => _stats = progress.Stats;

        public Transform GetTransform()
            => transform;

        private void OnAttack()
        {
            for (int i = 0; i < Hit(); i++)
                ProcessAttack(i);

            _animator.SetAnimatorSpeed(1);
            _isAttacking = false;
        }

        private void ProcessAttack(int index)
        {
            _hits[index].transform.parent.GetComponent<IHealth>().TakeDamage(_stats.Damage);
            ShowFx(index);
        }

        private void ShowFx(int index)
        {
            Instantiate(_bloodFx.gameObject, _hits[index].attachedRigidbody.position + Vector3.up * _offset,
                Quaternion.identity);
        }

        private int Hit()
            => Physics.OverlapSphereNonAlloc(_attackPoint.Position, _stats.AttackRadius, _hits, _layerMask);
    }
}