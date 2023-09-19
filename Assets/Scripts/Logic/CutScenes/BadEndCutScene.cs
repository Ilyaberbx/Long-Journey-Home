using System;
using DG.Tweening;
using Extensions;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.State;
using Logic.Animations;
using Logic.Camera;
using Logic.Car;
using Logic.Player;
using UI.Services.Factory;
using UnityEngine;
using Zenject;

namespace Logic.CutScenes

{
    public class BadEndCutScene : BaseCutScene
    {
        [SerializeField] private CutSceneCameraTransitionData[] _transitionDatas;
        [SerializeField] private CarLights _carLights;
        [SerializeField] private Transform _bear;
        [SerializeField] private Transform _door;
        [SerializeField] private Transform _bearLookPoint;
        [SerializeField] private Transform[] _bearWayPoints;
        [SerializeField] private float[] _bearMovingDurations;
        private CanvasGroup _eyeCurtain;
        private ICameraService _cameraService;
        private IUIFactory _uiFactory;
        private BearAnimator _bearAnimator;
        private IGameStateMachine _stateMachine;

        protected override void OnAwake()
        {
            _bear.gameObject.SetActive(false);

            if (IsCutScenePassed())
                DisableTriggers();
            else
                Initialize();
        }

        private void Initialize()
        {
            _bearAnimator = _bear.GetComponent<BearAnimator>();
            SpawnEyeCurtain();
        }

        [Inject]
        public void Construct(ICameraService cameraService, IUIFactory uiFactory, IGameStateMachine stateMachine)
        {
            _cameraService = cameraService;
            _uiFactory = uiFactory;
            _stateMachine = stateMachine;
        }

        public override void StartCutScene(Transform player, Action onCutSceneEnded)
        {
            _bear.gameObject.SetActive(true);

            HeroToggle heroToggle = player.GetComponent<HeroToggle>();

            HideEquip(player);
            _sequence = DOTween.Sequence();
            _sequence.AppendCallback(DisableTriggers);
            _sequence.Append(OpenDoor());
            _sequence.AppendCallback(() => ChangeCamera(_transitionDatas[0]));
            _sequence.AppendInterval(_transitionDatas[0].BlendTime);
            _sequence.Append(CloseDoor());
            _sequence.AppendInterval(1f);
            _sequence.Append(_carLights.KickstartLights(3f, 2f));
            _sequence.Append(_carLights.KickstartLights(3f, 4f));
            _sequence.Append(_carLights.KickstartLights(1f, 1f));
            _sequence.AppendCallback(() => MoveBearTo(_bearWayPoints[0], _bearMovingDurations[0]));
            _sequence.AppendInterval(1f);
            _sequence.AppendCallback(() => ChangeCamera(_transitionDatas[1]));
            _sequence.AppendInterval(_transitionDatas[1].BlendTime + 1f);
            _sequence.AppendCallback(() => ChangeCamera(_transitionDatas[2]));
            _sequence.AppendInterval(_transitionDatas[2].BlendTime);
            _sequence.AppendCallback(() => MoveBearTo(_bearWayPoints[1], _bearMovingDurations[1]));
            _sequence.Append(_carLights.KickstartLights(2f, 1f));
            _sequence.Append(_carLights.KickstartLights(3f, 2f));
            _sequence.AppendCallback(() => _bearAnimator.Move(0));
            _sequence.AppendCallback(() => _bear.LookAt(_bearLookPoint.position));
            _sequence.AppendInterval(1f);
            _sequence.AppendCallback(() => ChangeCamera(_transitionDatas[3]));
            _sequence.AppendInterval(_transitionDatas[3].BlendTime - 0.5f);
            _sequence.AppendCallback(() => _bearAnimator.PlayRoar());
            _sequence.AppendInterval(0.8f);
            _sequence.Append(ToggleEyeCurtain(1, 0.7f));
            _sequence.AppendInterval(1f);
            _sequence.AppendCallback(() => EnterEndingState(heroToggle));
        }

        private void MoveBearTo(Transform destination, float duration)
        {
            _bearAnimator.Move(1f);
            Vector3 position = destination.position;

            _bear.DOMove(position, duration).SetEase(Ease.Linear);
        }

        private void EnterEndingState(HeroToggle heroToggle)
            => _stateMachine.Enter<GameEndState, HeroToggle, EndingType>(heroToggle, EndingType.BadEnd);

        private void HideEquip(Transform player)
            => player.GetComponent<HeroEquiper>().ClearUp();

        private Tween CloseDoor()
            => ChangeDoorRotation(0);

        private Tween OpenDoor()
            => ChangeDoorRotation(70);

        private Tween ChangeDoorRotation(float value)
            => _door.DOLocalRotate(Vector3.zero.AddY(value), 1f).SetEase(Ease.OutExpo);

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
    }
}