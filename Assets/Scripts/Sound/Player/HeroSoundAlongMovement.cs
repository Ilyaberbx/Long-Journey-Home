using Infrastructure.Services.AssetManagement;
using Logic.Gravity;
using Logic.Player;
using Logic.Raycast;
using Sound.SoundSystem;
using Sound.SoundSystem.Operators.Variations;
using UnityEngine;

namespace Sound.Player
{
    public class HeroSoundAlongMovement : MonoBehaviour
    {
        [SerializeField] private SoundOperations _soundOperations;
        [SerializeField] private HeroMover _heroMover;
        [SerializeField] private GroundRayCastObserver _groundObserver;
        [SerializeField] private float _footStepSoundCoolDown;
        [SerializeField] private Gravity _gravity;
        private float _footStepTicker;
        private float _currentCoolDown;
        private IAssetProvider _assetProvider;


        private void Awake()
        {
            _gravity.OnFell += PlayJumpSound;
            _groundObserver.OnRayHit += PlayFootStepSound;
            _currentCoolDown = _footStepSoundCoolDown;
        }

        private void OnDestroy()
        {
            _groundObserver.OnRayHit -= PlayFootStepSound;
            _gravity.OnFell -= PlayJumpSound;
        }
        

        private void PlayFootStepSound(Ground ground)
        {
            _footStepTicker += Time.deltaTime;

            SurfaceType surface = ground.SurfaceType;

            if (!IsCooledDown() || !_heroMover.IsMoving())
                return;

            ApplySprintingCoefficient();
            PlayStepSound(surface);
            Reset();
        }

        private void PlayStepSound(SurfaceType surface) 
            => _soundOperations.PlaySound<MoveOperatorHandleSurface>(surface);

        private void ApplySprintingCoefficient()
        {
            if (_heroMover.IsSprinting())
                _currentCoolDown = _footStepSoundCoolDown / _heroMover.SprintingCoefficient;
            else
                _currentCoolDown = _footStepSoundCoolDown;
        }

        private void PlayJumpSound() 
            => _soundOperations.PlaySound<JumpSoundOperator>();


        private bool IsCooledDown()
            => _footStepTicker >= _currentCoolDown;

        private void Reset()
            => _footStepTicker = 0;
        
        
    }
}