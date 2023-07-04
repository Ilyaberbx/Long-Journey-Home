using Data;
using Extensions;
using Infrastructure.Services.Input;
using Infrastructure.Services.SaveLoad;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Logic.Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Gravity.Gravity))]
    public class HeroMover : MonoBehaviour,ISavedProgressWriter
    {
        private const float GravityConst = -20f;

        public bool CanJump { get; set; } = true;
        
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _sprintingCoefficient;

        private IInputService _input;
        private Gravity.Gravity _gravity;

        private UnityEngine.Camera _camera;

        [Inject]
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
            Vector3 input = new Vector3(_input.Horizontal, 0, _input.Vertical);
            Vector3 movement = CalculateMovement(input);

            if (_input.IsSprinting())
                movement *= _sprintingCoefficient;

            _characterController.Move(movement * (_speed * Time.deltaTime));
        }

        private Vector3 CalculateMovement(Vector3 input) 
            => transform.forward * input.z + transform.right * input.x;

        private void Jump()
        {
            if(!CanJump)
                return;
            
            Vector3 velocity = _gravity.GetVelocity();
            velocity.y += Mathf.Sqrt(_jumpHeight * (-3.0f * GravityConst));
            _gravity.SetVelocity(velocity);
        }

        public void UpdateProgress(PlayerProgress progress)
            => UpdatePlayerPositionOnLevelProgress(progress);

        private void UpdatePlayerPositionOnLevelProgress(PlayerProgress progress)
        {
            if (CurrentLevel() != progress.WorldData.PositionOnLevel.Level) return;

            progress.WorldData.PositionOnLevel.Position = transform.position.AsVector3Data();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (!CanLoadPosition(progress)) return;
            
            Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;

            if (savedPosition != null)
                WarpPlayerPosition(savedPosition);
        }

        private bool CanLoadPosition(PlayerProgress progress) 
            => CurrentLevel() == progress.WorldData.PositionOnLevel.Level && progress.WorldData.PositionOnLevel.Position != null && progress.WorldData.PositionOnLevel.Position.AsUnityVector() != Vector3.zero;

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