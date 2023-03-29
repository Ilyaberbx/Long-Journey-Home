using DG.Tweening;
using Logic.Inventory;
using Logic.Player;
using UnityEngine;

namespace Logic.Weapons
{
    public class Axe : BaseItem, IWeapon, IEquippable
    {
        private const string HittableLayerName = "Hittable";
        public IWeaponAnimator WeaponAnimator => _animator;
        public ItemType ItemType => Type;

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
        private CheckPoint _attackPoint;

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

        public override void Use(HeroMover player)
        {
            var equipSwitcher = player.GetComponent<HeroEquipSwitcher>();
            var attack = player.GetComponent<HeroAttack>();

            if (equipSwitcher.IsAlreadyEquip(Type))
                Destroy(gameObject);

            transform.SetParent(equipSwitcher.EquipmentContainer);
            transform.position = equipSwitcher.EquipPosition;
            _attackPoint = attack.AttackPoint;
        }
    }
}