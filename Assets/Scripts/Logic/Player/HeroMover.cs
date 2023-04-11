using Data;
using Extensions;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Infrastructure.Services.Input;
using Infrastructure.Services.SaveLoad;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Logic.Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Gravity.Gravity))]
    public class HeroMover : MonoBehaviour,ISavedProgressWriter
    {
        private const float GravityConst = -10f;
        
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _sprintingCoefficient;

        private IInputService _input;
        private Gravity.Gravity _gravity;

        private UnityEngine.Camera _camera;

        public void Construct(IInputService inputService) 
            => _input = inputService;

        private void Awake()
        {
            _gravity = GetComponent<Gravity.Gravity>();
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

        public void UpdateProgress(PlayerProgress progress)
            => UpdatePlayerPositionOnLevelProgress(progress);

        private void UpdatePlayerPositionOnLevelProgress(PlayerProgress progress)
        {
            progress.WorldData.PositionOnLevel = new PositionOnLevel(SceneManager.GetActiveScene().name,
                transform.position.AsVector3Data());
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (CurrentLevel() != progress.WorldData.PositionOnLevel.Level) return;
            
            Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;

            if (savedPosition != null)
                WarpPlayerPosition(savedPosition);
        }

        private void WarpPlayerPosition(Vector3Data to)
        {
            _characterController.enabled = false;
            transform.position = to.AsUnityVector().AddY(_characterController.height);
            _characterController.enabled = true;
        }

        private string CurrentLevel()
            => SceneManager.GetActiveScene().name;
    }
}