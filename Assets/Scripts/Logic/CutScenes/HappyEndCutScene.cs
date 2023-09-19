using System;
using DG.Tweening;
using Extensions;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.State;
using Logic.Camera;
using Logic.Car;
using Logic.Player;
using UI.Services.Factory;
using UnityEngine;
using Zenject;

namespace Logic.CutScenes
{
    public class HappyEndCutScene : BaseCutScene
    {
        [SerializeField] private CutSceneCameraTransitionData[] _transitionDatas;
        [SerializeField] private Transform _door;
        [SerializeField] private CarLights _carLights;
        private ICameraService _cameraService;
        private IUIFactory _uiFactory;
        private CanvasGroup _eyeCurtain;
        private IGameStateMachine _stateMachine;

        [Inject]
        public void Construct(ICameraService cameraService, IUIFactory uiFactory,IGameStateMachine stateMachine)
        {
            _cameraService = cameraService;
            _uiFactory = uiFactory;
            _stateMachine = stateMachine;
        }

        protected override void OnAwake()
        {
            if (IsCutScenePassed())
                DisableTriggers();
            else
                SpawnEyeCurtain();
        }

        public override void StartCutScene(Transform player, Action onCutSceneEnded)
        {
            Debug.Log("Enter Happy CutScene");
            HeroCameraWrapper cameraWrapper = player.GetComponent<HeroCameraWrapper>();
            HeroEquiper equiper = player.GetComponent<HeroEquiper>();
            HeroToggle heroToggle = player.GetComponent<HeroToggle>();
            _sequence = DOTween.Sequence();
            _sequence.AppendCallback(DisableTriggers);
            _sequence.AppendCallback(equiper.ClearUp);
            _sequence.AppendCallback(ParentEquipmentToMain(cameraWrapper));
            _sequence.Append(OpenDoor());
            _sequence.AppendCallback(() => ChangeCamera(_transitionDatas[0]));
            _sequence.AppendInterval(_transitionDatas[0].BlendTime + 1.4f);
            _sequence.Append(CloseDoor());
            _sequence.AppendInterval(1.2f);
            _sequence.Append(_carLights.KickstartLights(3f, 2f));
            _sequence.AppendInterval(2f);
            _sequence.Append(_carLights.KickstartLights(2f, 2f));
            _sequence.AppendInterval(3f);
            _sequence.Append(_carLights.KickstartLights(4f, 4f));
            _sequence.AppendInterval(1f);
            _sequence.AppendCallback(() => _carLights.ToggleLights(1f, 500000));
            _sequence.AppendInterval(2f);
            _sequence.Append(ToggleEyeCurtain(1,1f));
            _sequence.AppendInterval(1f);
            _sequence.AppendCallback(() => EnterEndingState(heroToggle));
        }

        private void EnterEndingState(HeroToggle heroToggle)
            => _stateMachine.Enter<GameEndState, HeroToggle, EndingType>(heroToggle, EndingType.HappyEnd);

        private void ChangeCamera(CutSceneCameraTransitionData data)
        {
            _cameraService.Brain.m_DefaultBlend.m_CustomCurve = data.BlendCurve;
            _cameraService.Brain.m_DefaultBlend.m_Time = data.BlendTime;
            _cameraService.ChangeCamerasPriority(data.Type);
        }

        private async void SpawnEyeCurtain()
        {
            GameObject handle = await _uiFactory.CreateCurtain();
            _eyeCurtain = handle.GetComponent<CanvasGroup>();
        }

        private Tween ToggleEyeCurtain(float value, float duration)
            => _eyeCurtain.DOFade(value, duration);

        private TweenCallback ParentEquipmentToMain(HeroCameraWrapper wrapper)
            => wrapper.ParentEquipmentToMainCamera;

        private Tween CloseDoor()
            => ChangeDoorRotation(0);

        private Tween OpenDoor()
            => ChangeDoorRotation(70);

        private Tween ChangeDoorRotation(float value)
            => _door.DOLocalRotate(Vector3.zero.AddY(value), 1f).SetEase(Ease.OutExpo);
    }
}