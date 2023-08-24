using System;
using System.Collections.Generic;
using DG.Tweening;
using Extensions;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.State;
using Logic.Camera;
using Logic.Car;
using UnityEngine;
using Zenject;

namespace Logic.CutScenes
{
    public class IntroCutScene : BaseCutScene
    {
        private const string IntroRoadScene = "IntroRoad";

        [SerializeField] private int _zigZagStartIndex;
        [SerializeField] private int _zigZagEndIndex;
        [SerializeField] private List<Transform> _carWayPoints;
        [SerializeField] private List<CutSceneCameraTransitionData> _cameraTransitions;
        [SerializeField] private Transform _car;
        [SerializeField] private PathType _pathSystem;
        [SerializeField] private float _carMovingDuration;
        [SerializeField] private Transform _door;


        private CarLights _carLights;
        private IGameStateMachine _stateMachine;
        private Vector3[] _wayPointPositions;
        private Tween _carMovingTween;
        private ICameraService _cameraService;


        [Inject]
        public void Construct(ICameraService cameraService, IGameStateMachine stateMachine)
        {
            _cameraService = cameraService;
            _stateMachine = stateMachine;
        }

        private void Awake()
        {
            _carLights = _car.GetComponentInChildren<CarLights>();
            _wayPointPositions = CollectWayPoints();
            Cursor.lockState = CursorLockMode.Locked;
            
            StartCutScene(_car, EntryRoadScene);
        }

        private Vector3[] CollectWayPoints()
        {
            Vector3[] wayPoints = new Vector3[_carWayPoints.Count];

            for (int i = 0; i < _carWayPoints.Count; i++)
                wayPoints[i] = _carWayPoints[i].position;

            return wayPoints;
        }

        private void EntryRoadScene()
            => _stateMachine.Enter<LoadLevelState, string>(IntroRoadScene);

        public override void StartCutScene(Transform car, Action onCutSceneEnded)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.AppendCallback(() => MoveCar(_carMovingDuration));
            sequence.AppendCallback(() => _carLights.ToggleLights(0f, 500000));
            sequence.AppendInterval(_carMovingDuration + 2f);
            sequence.AppendCallback(() => _carLights.ToggleLights(3f, 0f));
            sequence.AppendInterval(8f);
            sequence.Append(_carLights.KickstartLights(3f, 2f));
            sequence.AppendInterval(2f);
            sequence.Append(_carLights.KickstartLights(2f, 2f));
            sequence.AppendInterval(3f);
            sequence.Append(_carLights.KickstartLights(4f, 4f));
            sequence.AppendInterval(8f);
            sequence.Append(OpenDoor());
            sequence.AppendInterval(4f);
            sequence.AppendCallback(() => ChangeCamera(_cameraTransitions[0]));
            sequence.AppendInterval(_cameraTransitions[0].BlendTime + 3f);
            sequence.AppendCallback(() => ChangeCamera(_cameraTransitions[1]));
            sequence.AppendInterval(_cameraTransitions[1].BlendTime + 3f);
            sequence.AppendCallback(() => onCutSceneEnded?.Invoke());
        }
        

        private Tween OpenDoor() 
            => _door.DOLocalRotate(Vector3.zero.AddY(70f), 1f).SetEase(Ease.OutExpo);

        private void MoveCar(float duration) =>
            _carMovingTween = _car.DOPath(_wayPointPositions, duration, _pathSystem).SetEase(Ease.Linear)
                .SetLookAt(0.01f)
                .OnWaypointChange(RotateToWayPoint);

        private void RotateToWayPoint(int index)
        {
            if (IsLastWayPoint(index))
            {
                _carMovingTween.DOTimeScale(0, 2f).SetEase(Ease.Linear);
                return;
            }

            if (IsStartOfZigZag(index))
                _carMovingTween.DOTimeScale(0.6f, 2f).SetEase(Ease.Linear);
            else if (IsEndOfZigZag(index)) 
                _carMovingTween.DOTimeScale(1, 2f).SetEase(Ease.Linear);
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