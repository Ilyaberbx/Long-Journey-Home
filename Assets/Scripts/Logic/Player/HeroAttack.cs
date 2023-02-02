using Data;
using Infrastructure.Services;
using Interfaces;
using UnityEngine;

namespace Logic.Player
{
    public class HeroAttack : MonoBehaviour, ISavedProgressReader
    {
        private const string HittableLayerName = "Hittable";

        [SerializeField] private CheckPoint _attackPoint;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private ParticleSystem _bloodFx;
        [SerializeField] private float _offset;

        private IWeaponAnimator _animator;
        private IInputService _input;
        private bool _isAttacking;

        private int _layerMask;
        private Collider[] _hits = new Collider[3];
        private Stats _stats;

        private void Awake()
        {
            _animator = GetComponentInChildren<IWeaponAnimator>();
            _input = ServiceLocator.Container.Single<IInputService>();
            _layerMask = 1 << LayerMask.NameToLayer(HittableLayerName);
        }

        private void Update()
        {
            if (_input.IsAttackButtonPressed() && !_isAttacking)
            {
                _animator.PlayAttack();
                _animator.SetAnimatorSpeed(_attackSpeed);
                _isAttacking = true;
            }
        }

        public void OnAttack()
        {
            for (int i = 0; i < Hit(); i++)
                PerformAttack(i);

            _animator.SetAnimatorSpeed(1);
            _isAttacking = false;
        }

        private void PerformAttack(int index)
        {
            _hits[index].transform.parent.GetComponent<IHealth>().TakeDamage(_stats.Damage);
            Instantiate(_bloodFx.gameObject, _hits[index].attachedRigidbody.position + Vector3.up * _offset, Quaternion.identity);
        }

        private int Hit()
            => Physics.OverlapSphereNonAlloc(_attackPoint.Position, _stats.AttackRadius, _hits, _layerMask);

        public void LoadProgress(PlayerProgress progress)
            => _stats = progress.Stats;
    }
}