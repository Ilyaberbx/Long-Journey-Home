using Data;
using Infrastructure.Services;
using Interfaces;
using ProjectSolitude.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Logic.Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Gravity))]
    public class HeroMover : MonoBehaviour
    {
        private const float GravityConst = -10f;
        
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _sprintingCoefficient;

        private IInputService _input;
        private Gravity _gravity;
        private UnityEngine.Camera _camera;

        private void Awake()
        {
            _input = ServiceLocator.Container.Single<IInputService>();
            _gravity = GetComponent<Gravity>();
            _camera = UnityEngine.Camera.main;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            Move();
            if (_input.IsJumped() && _gravity.TryCatchGround())
                Jump();
        }

        private void Move()
        {
            Vector3 movement = new Vector3(_input.Horizontal, 0, _input.Vertical);
            movement = _camera.transform.forward * movement.z + _camera.transform.right * movement.x;

            if (_input.IsSprinting())
                movement *= _sprintingCoefficient;

            _characterController.Move(movement * (_speed * Time.deltaTime));
        }

        private void Jump()
        {
            Vector3 velocity = _gravity.GetVelocity();
            velocity.y += Mathf.Sqrt(_jumpHeight * (-3.0f * GravityConst));
            _gravity.SetVelocity(velocity);
        }
        
    }
}