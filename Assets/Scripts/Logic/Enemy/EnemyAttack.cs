using System.Linq;
using DG.Tweening;
using Logic.Animations;
using Logic.Gravity;
using Logic.Player;
using UnityEngine;

namespace Logic.Enemy
{
    [RequireComponent(typeof(BaseEnemyAnimator))]
    public class EnemyAttack : MonoBehaviour
    {
        private const string PlayerLayer = "Player";
        public bool IsAttacking => _isAttacking;
        
        [SerializeField] private BaseEnemyAnimator _animator;
        [SerializeField] private CheckPoint _checkPoint;
        [SerializeField] private float _rotationToPlayerDuration;

        private readonly Collider[] _colliders = new Collider[1];
        private Transform _playerTransform;
        private int _damage;
        private float _attackCoolDown;
        private float _currentCoolDown;
        private bool _isAttacking;
        private int _layerMask;
        private bool _attackIsActive;

        public void Construct(Transform playerTransform,int damage,float attackCoolDown)
        {
            _damage = damage;
            _attackCoolDown = attackCoolDown;
            _playerTransform = playerTransform;
        }

        private void Awake() 
            => _layerMask = 1 << LayerMask.NameToLayer(PlayerLayer);

        private void Update()
        {
            UpdateCoolDown();

            if (IsReadyToAttack())
                PerformAttack();
        }

        public void DisableAttack() 
            => _attackIsActive = false;

        public void EnableAttack() 
            => _attackIsActive = true;

        private void OnAttack()
        {
            if (Hit(out Collider collider))
            {
                IHealth health = collider.transform.GetComponent<IHealth>();
                health.TakeDamage(_damage);
            }
        }

        private void OnAttackEnded()
        {
            Debug.Log("OnAttackEnded");
            
            _currentCoolDown = _attackCoolDown;
            _isAttacking = false;
        }

        private bool Hit(out Collider collider)
        {
            int hitsCount =
                Physics.OverlapSphereNonAlloc(_checkPoint.Position, _checkPoint.Radius, _colliders, _layerMask);

            collider = _colliders.FirstOrDefault();

            return hitsCount > 0;
        }

        private void UpdateCoolDown()
        {
            if (!IsCoolDownEnded())
                _currentCoolDown -= Time.deltaTime;
        }

        private bool IsReadyToAttack()
            => _attackIsActive && IsCoolDownEnded() && !_isAttacking;

        private void PerformAttack()
        {
            transform.DOLookAt(_playerTransform.position, _rotationToPlayerDuration,AxisConstraint.Y);
            _animator.PlayRandomAttack();

            _isAttacking = true;
        }

        private bool IsCoolDownEnded()
            => _currentCoolDown <= 0;
        
    }
}