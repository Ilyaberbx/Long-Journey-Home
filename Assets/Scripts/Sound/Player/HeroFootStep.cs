using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Services.AssetManagement;
using Logic.Gravity;
using Logic.Player;
using UnityEngine;
using Zenject;

namespace Sound.Player
{
    public class HeroFootStep : MonoBehaviour
    {
        [SerializeField] private HeroMover _heroMover;
        [SerializeField] private float _footStepSoundCoolDown;
        [SerializeField] private AudioSource _footStepSoundSource;
        [SerializeField] private AudioClip _jumpSound;
        [SerializeField] private Gravity _gravity;
        [SerializeField] private FootStepReferenceData[] _footStepReferenceData;

        private FootStepSoundData[] _footStepSoundData;
        private float _footStepTicker;
        private float _currentCoolDown;
        private IAssetProvider _assetProvider;

        [Inject]
        public void Construct(IAssetProvider assetProvider)
            => _assetProvider = assetProvider;

        private async void Awake()
        {
            _gravity.OnGrounded += PlayFootStepSound;
            _heroMover.OnJumped += PlayJumpSound;

            _currentCoolDown = _footStepSoundCoolDown;
            await LoadAssets();
        }

        private void OnDestroy()
        {
            _gravity.OnGrounded -= PlayFootStepSound;
            _heroMover.OnJumped -= PlayJumpSound;
        }

        private async Task LoadAssets()
        {
            _footStepSoundData = new FootStepSoundData[_footStepReferenceData.Length];

            for (int i = 0; i < _footStepReferenceData.Length; i++)
            {
                FootStepReferenceData footStepData = _footStepReferenceData[i];
                AudioClip[] stepSounds = await _assetProvider.Load<AudioClip>(footStepData.StepSoundsReference);
                _footStepSoundData[i] = new FootStepSoundData(stepSounds, footStepData.SurfaceType);
            }
        }

        private void PlayFootStepSound(SurfaceType surface)
        {
            _footStepTicker += Time.deltaTime;

            if (!IsCooledDown() || !_heroMover.IsMoving())
                return;

            ApplySprintingCoefficient();
            StopPreviousSound();

            FootStepSoundData stepReferenceData = GetFootStepDataBySurface(surface);

            if (stepReferenceData == null)
                return;

            AudioClip stepSound = GetRandomStepSound(stepReferenceData);

            Play(stepSound);
            Reset();
        }

        private void ApplySprintingCoefficient()
        {
            if (_heroMover.IsSprinting())
                _currentCoolDown = _footStepSoundCoolDown / _heroMover.SprintingCoefficient;
            else
                _currentCoolDown = _footStepSoundCoolDown;
        }

        private void PlayJumpSound()
        {
            StopPreviousSound();
            Play(_jumpSound);
        }

        private void StopPreviousSound()
            => _footStepSoundSource.Stop();

        private bool IsCooledDown()
            => _footStepTicker >= _currentCoolDown;

        private void Reset()
            => _footStepTicker = 0;


        private void Play(AudioClip clip)
        {
            _footStepSoundSource.pitch = Random.Range(0.8f, 1.2f);
            _footStepSoundSource.volume = Random.Range(0.8f, 1f);
            _footStepSoundSource.clip = clip;
            _footStepSoundSource.Play();
        }

        private AudioClip GetRandomStepSound(FootStepSoundData stepSoundData)
            => stepSoundData.StepSounds[Random.Range(0, stepSoundData.StepSounds.Length)];

        private FootStepSoundData GetFootStepDataBySurface(SurfaceType surface)
            => _footStepSoundData.FirstOrDefault(footStepData => footStepData.SurfaceType == surface);
    }
}