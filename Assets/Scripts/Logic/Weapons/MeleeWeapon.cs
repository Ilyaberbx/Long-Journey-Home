using DG.Tweening;
using Logic.Gravity;
using Logic.Inventory.Item;
using Logic.Player;
using UnityEngine;

namespace Logic.Weapons
{
    public class MeleeWeapon : BaseEquippableItem, IWeapon
    {
        private const string HittableLayerName = "Hittable";
        private const int AttackMinIndex = 0;
        private const int AttackMaxIndex = 3;

        [SerializeField] private CheckPoint _attackPoint;
        [SerializeField] private int _damage;
        [SerializeField] private float _attackRadius;
        [SerializeField] private GameObject _bloodFx;

        private readonly Collider[] _hits = new Collider[3];
        private IWeaponAnimator _animator;
        private bool _isAttacking;

        private int _layerMask;
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

            _animator.PlayAttack(CalculateAttackIndex());
            _isAttacking = true;
        }

        private int CalculateAttackIndex() 
            => Random.Range(AttackMinIndex,AttackMaxIndex);

        public override void Appear()
        {
            _isAttacking = false;
            transform.DOScale(_cachedScale, 0.2f);
        }

        public override void Hide()
            => Destroy(gameObject);

        private void OnAttack()
        {
            for (int i = 0; i < Hit(); i++)
                ProcessAttack(i);

            _isAttacking = false;
        }

        private void ProcessAttack(int index)
        {
            _hits[index].transform.parent.GetComponent<IHealth>().TakeDamage(_damage);
            ShowFx(index);
        }

        private void ShowFx(int index)
        {
            Vector3 position = transform.position;
            Vector3 closestPoint = _hits[index].ClosestPoint(position);

            Instantiate(_bloodFx.gameObject, closestPoint + Vector3.down * 8f, Quaternion.LookRotation(-position));
        }

        private int Hit()
            => Physics.OverlapSphereNonAlloc(_attackPoint.Position, _attackRadius, _hits, _layerMask);
    }
}