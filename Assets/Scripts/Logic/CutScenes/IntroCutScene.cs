using System;
using System.Collections.Generic;
using DG.Tweening;
using Extensions;
using Infrastructure.Services.Dialogue;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.State;
using Logic.Camera;
using Logic.Car;
using Logic.DialogueSystem;
using UnityEngine;
using Zenject;

namespace Logic.CutScenes
{
    public class IntroCutScene : BaseCutScene
    {
        private const string IntroRoadScene = "IntroRoad";

        [SerializeField] private List<Transform> _carWayPoints;
        [SerializeField] private List<CutSceneCameraTransitionData> _cameraTransitions;
        [SerializeField] private Dialogue[] _dialogues;
        [SerializeField] private Transform _car;
        [SerializeField] private PathType _pathSystem;
        [SerializeField] private int _zigZagStartIndex;
        [SerializeField] private int _zigZagEndIndex;
        [SerializeField] private float _carMovingDuration;
        [SerializeField] private Transform _door;

        private CarLights _carLights;
        private IGameStateMachine _stateMachine;
        private Vector3[] _wayPointPositions;
        private Tween _carMovingTween;
        private Tween _changingTimeScaleTween;
        private ICameraService _cameraService;
        private IDialogueService _dialogueService;
        private int _wayPointIndex;

        [Inject]
        public void Construct(ICameraService cameraService, IDialogueService dialogueService,
            IGameStateMachine stateMachine)
        {
            _cameraService = cameraService;
            _dialogueService = dialogueService;
            _stateMachine = stateMachine;
        }

        protected override void OnAwake()
        {
            _carLights = _car.GetComponentInChildren<CarLights>();
            _wayPointPositions = CollectWayPoints();
            Cursor.lockState = CursorLockMode.Locked;

            StartCutScene(_car, EntryRoadScene);
        }

        public override void HandlePause(bool isPaused)
        {
            base.HandlePause(isPaused);

            if (_carMovingTween == null) return;

            if (isPaused)
            {
                _carMovingTween.timeScale = 0;
                
                ResetChangingTimeScaleTween();
                return;
            }

            HandleWayPointTimeScale(_wayPointIndex);
        }

        private void ResetChangingTimeScaleTween()
        {
            if (_changingTimeScaleTween != null)
                DOTween.Kill(_changingTimeScaleTween);
        }

        private Vector3[] CollectWayPoints()
        {
            Vector3[] wayPoints = new Vector3[_carWayPoints.Count];

            for (int i = 0; i < _carWayPoints.Count; i++)
                wayPoints[i] = _carWayPoints[i].position;

            return wayPoints;
        }

        private void EntryRoadScene()
        {
            Debug.Log("End intro");
            _stateMachine.Enter<LoadLevelState, string>(IntroRoadScene);
        }

        public override void StartCutScene(Transform car, Action onCutSceneEnded)
        {
            _sequence = DOTween.Sequence();
            _sequence.AppendCallback(() => MoveCar(_carMovingDuration));
            _sequence.AppendCallback(() => _carLights.ToggleLights(0f, 500000));
            _sequence.AppendInterval(_carMovingDuration + 2f);
            _sequence.AppendCallback(() => _carLights.ToggleLights(3f, 0f));
            _sequence.AppendInterval(8f);
            _sequence.Append(_carLights.KickstartLights(3f, 2f));
            _sequence.AppendInterval(2f);
            _sequence.AppendCallback(() => StartDialogue(_dialogues[0]));
            _sequence.Append(_carLights.KickstartLights(2f, 2f));
            _sequence.AppendInterval(3f);
            _sequence.Append(_carLights.KickstartLights(4f, 4f));
            _sequence.AppendInterval(8f);
            _sequence.Append(OpenDoor());
            _sequence.AppendInterval(4f);
            _sequence.AppendCallback(() => ChangeCamera(_cameraTransitions[0]));
            _sequence.AppendInterval(_cameraTransitions[0].BlendTime + 3f);
            _sequence.AppendCallback(() => ChangeCamera(_cameraTransitions[1]));
            _sequence.AppendInterval(_cameraTransitions[1].BlendTime + 3f);
            _sequence.AppendCallback(() => onCutSceneEnded?.Invoke());
        }

        private void StartDialogue(Dialogue dialogue) 
            => _dialogueService.StartDialogue(dialogue);


        private Tween OpenDoor()
            => _door.DOLocalRotate(Vector3.zero.AddY(70f), 1f).SetEase(Ease.OutExpo);

        private void MoveCar(float duration) =>
            _carMovingTween = _car.DOPath(_wayPointPositions, duration, _pathSystem).SetEase(Ease.Linear)
                .SetLookAt(0.01f)
                .OnWaypointChange(HandleWayPointTimeScale);

        private void HandleWayPointTimeScale(int index)
        {
            _wayPointIndex = index;

            if (IsLastWayPoint(_wayPointIndex))
            {
                _changingTimeScaleTween = _carMovingTween.DOTimeScale(0, 2f).SetEase(Ease.Linear);
                return;
            }

            if (IsStartOfZigZag(_wayPointIndex))
                _changingTimeScaleTween = _carMovingTween.DOTimeScale(0.6f, 2f).SetEase(Ease.Linear);
            else if (IsEndOfZigZag(_wayPointIndex))
                _changingTimeScaleTween = _carMovingTween.DOTimeScale(1, 2f).SetEase(Ease.Linear);
            else
                _carMovingTween.timeScale = 1;
        }

        private void ChangeCamera(CutSceneCameraTransitionData data)
        {
            _cameraService.Brain.m_DefaultBlend.m_CustomCurve = data.BlendCurve;
            _cameraService.Brain.m_DefaultBlend.m_Time = data.BlendTime;
            _cameraService.ChangeCamerasPriority(data.Type);
        }

        private bool IsEndOfZigZag(int index)
            => _zigZagEndIndex == index;

        private bool IsStartOfZigZag(int index)
            => _zigZagStartIndex == index;

        private bool IsLastWayPoint(int index)
            => _carWayPoints.Count <= index + 1;
    }
}