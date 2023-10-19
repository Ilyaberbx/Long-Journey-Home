using System;
using Logic.Animations;
using UnityEngine;

namespace Logic.Gravity
{
    public class Gravity : MonoBehaviour
    {
        public event Action OnFell; 

        private const float GroundedGravityConst = -2f;
        private const float GravityCoefficientConst = 1.05f;

        [SerializeField] private float _gravity = 120f;
        [SerializeField] private CheckPoint _checkingGroundPoint;
        [SerializeField] private float _jumpingLevitationTime;
        [SerializeField] private CharacterController _characterController;
        private Vector3 _velocity;
        private ICameraAnimator _cameraAnimator;
        private bool _isGrounded;
        private float _timeInLevitation;

        public Vector3 GetVelocity()
            => _velocity;

        public void SetVelocity(Vector3 velocity)
            => _velocity = velocity;

        public void Construct(ICameraAnimator cameraAnimator)
            => _cameraAnimator = cameraAnimator;

        private void Awake() => Init();

        private void LateUpdate()
        {
            _timeInLevitation += Time.deltaTime;
            CalculateGravity();
        }


        public bool TryCatchGround()
        {
            Collider[] hits = Physics.OverlapSphere(_checkingGroundPoint.Position, _checkingGroundPoint.Radius);

            foreach (Collider check in hits)
            {
                if (check.transform == null) continue;

                Ground ground = check.transform.GetComponentInParent<Ground>();
                
                if (ground == null) continue;

                if (IsBigFall())
                {
                    _cameraAnimator.PlayGrounded();
                    OnFell?.Invoke();
                }

                _timeInLevitation = 0f;
                return _isGrounded = true;
            }

            return _isGrounded = false;
        }
        

        private bool IsBigFall() 
            => !_isGrounded && _timeInLevitation >= _jumpingLevitationTime;

        private void Init()
            => _gravity *= GravityCoefficientConst;


        private void CalculateGravity()
        {
            if (TryCatchGround() && _velocity.y < 0)
                _velocity.y = GroundedGravityConst;

            _velocity.y -= _gravity * Time.deltaTime;
            _characterController.Move(_velocity * Time.deltaTime);
        }
    }
}