﻿using Logic.Animations;
using UnityEngine;

namespace Logic.Gravity
{
    public class Gravity : MonoBehaviour
    {
        private const float GroundedGravityConst = -2f;
        private const float GravityCoefficientConst = 1.05f;

        [SerializeField] private float _gravity = 120f;
        [SerializeField] private CheckPoint _checkingGroundPoint;

        [SerializeField] private CharacterController _characterController;
        private Vector3 _velocity;
        private ICameraAnimator _cameraAnimator;
        private bool _isGrounded;

        public Vector3 GetVelocity()
            => _velocity;

        public void SetVelocity(Vector3 velocity)
            => _velocity = velocity;

        public void Construct(ICameraAnimator cameraAnimator)
            => _cameraAnimator = cameraAnimator;

        private void Awake() => Inititalize();

        private void LateUpdate() => CalculateGravity();


        public bool TryCatchGround()
        {
            Collider[] hits = Physics.OverlapSphere(_checkingGroundPoint.Position, _checkingGroundPoint.Radius);

            foreach (var check in hits)
            {
                if (check.transform != null)

                    if (check.transform.GetComponentInParent<Ground>() != null)
                    {
                        if (!_isGrounded)
                            _cameraAnimator.PlayGrounded();
                        
                        return _isGrounded = true;
                    }
            }

            return _isGrounded = false;
        }

        private void Inititalize()
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