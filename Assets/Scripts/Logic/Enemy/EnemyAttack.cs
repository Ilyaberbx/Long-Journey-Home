using System.Linq;
using DG.Tweening;
using Infrastructure.Services.Pause;
using Logic.Animations;
using Logic.Gravity;
using Logic.Player;
using UnityEngine;
using Zenject;

namespace Logic.Enemy
{
    [RequireComponent(typeof(BaseEnemyAnimator))]
    public class EnemyAttack : MonoBehaviour
    {
        private const string PlayerLayer = "Player";
  
        [SerializeField] private AgentMoveToPlayer _agent;
        [SerializeField] private BaseEnemyAnimator _animator;
        [SerializeField] private CheckPoint _checkPoint;
        [SerializeField] private float _rotationToPlayerDuration;

        private IPauseService _pauseService;
        private readonly Collider[] _colliders = new Collider[3];
        private Transform _playerTransform;
        private int _damage;
        private float _attackCoolDown;
        private float _currentCoolDown;
        private bool _isAttacking;
        private int _layerMask;
        private bool _attackIsActive;

        [Inject]
        public void Construct(IPauseService pauseService) 
            => _pauseService = pauseService;

        public void Init(Transform playerTransform,int damage,float attackCoolDown)
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
            Debug.Log("OnAttack");
            if (Hit(out Collider collider) && collider.TryGetComponent(out HeroHealth health))
                health.TakeDamage(_damage);
        }

        private void OnAttackEnded()
        {
            _agent.Resume();
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
            if(_pauseService.IsPaused)
                return;
            
            _agent.Stop();
            LookAtVictim();
            _animator.PlayRandomAttack();

            _isAttacking = true;
        }

        private void LookAtVictim() =>
            transform.DOLookAt(_playerTransform.position, _rotationToPlayerDuration, AxisConstraint.Y, transform.up)
                .SetEase(Ease.OutExpo);

        private bool IsCoolDownEnded()
            => _currentCoolDown <= 0;
        
    }
}