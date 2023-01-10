using System.Linq;
using DG.Tweening;
using ProjectSolitude.Extensions;
using ProjectSolitude.Infrastructure;
using ProjectSolitude.Interfaces;
using UnityEngine;

namespace ProjectSolitude.Logic
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyAttack : MonoBehaviour
    {
        private const string PlayerLayer = "Player";

        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private float _attackCoolDown;
        [SerializeField] private CheckPoint _checkPoint;
        [SerializeField] private float _rotationToPlayerDuration;

        private IGameFactory _gameFactory;
        private Transform _playerTransform;
        private float _currentCoolDown;
        private bool _isAttacking;
        private int _layerMask;
        private Collider[] _colliders = new Collider[1];
        private bool _attackIsActive;

        private void Awake()
        {
            _gameFactory = ServiceLocator.Container.Single<IGameFactory>();
            _gameFactory.OnHeroCreated += OnHeroCreated;
            _layerMask = 1 << LayerMask.NameToLayer(PlayerLayer);
        }

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
                Debug.Log("Attack");
                PhysicsDebug.DrawDebug(_checkPoint.Position, _checkPoint.Radius, 1f);
            }
        }

        private void OnAttackEnded()
        {
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
            transform.DOLookAt(_playerTransform.position, _rotationToPlayerDuration);
            _animator.PlayAttack();

            _isAttacking = true;
        }

        private bool IsCoolDownEnded()
            => _currentCoolDown <= 0;

        private void OnHeroCreated()
            => _playerTransform = _gameFactory.HeroGameObject.transform;
    }
}