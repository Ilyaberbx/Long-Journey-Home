using System;
using System.Collections.Generic;
using DG.Tweening;
using Infrastructure.Services.SaveLoad;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.State;
using Logic.Camera;
using Logic.Player;
using UI.Services.Factory;
using UnityEngine;
using Zenject;

namespace Logic.CutScenes
{
    public class HouseCutScene : BaseCutScene
    {
        [SerializeField] private List<CutSceneCameraTransitionData> _camerasTransitionData;
        [SerializeField] private string _transferTo;
        private ICameraService _cameraService;
        private IUIFactory _uiFactory;
        private CanvasGroup _eyeCurtain;
        private IGameStateMachine _stateMachine;
        private ISaveLoadService _saveLoadService;

        [Inject]
        public void Construct(ICameraService cameraService,IUIFactory uiFactory,IGameStateMachine stateMachine,ISaveLoadService saveLoadService)
        {
            _cameraService = cameraService;
            _uiFactory = uiFactory;
            _stateMachine = stateMachine;
            _saveLoadService = saveLoadService;
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
            HeroCameraWrapper cameraWrapper = player.GetComponent<HeroCameraWrapper>();
            _sequence = DOTween.Sequence();
            _sequence.AppendCallback(ParentEquipmentToMain(cameraWrapper));
            _sequence.AppendCallback(DisableTriggers);
            _sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[0]));
            _sequence.AppendInterval(_camerasTransitionData[0].BlendTime + 1f);
            _sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[1]));
            _sequence.AppendInterval(_camerasTransitionData[1].BlendTime + 2f);
            _sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[2]));
            _sequence.AppendInterval(_camerasTransitionData[2].BlendTime);
            _sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[3]));
            _sequence.AppendInterval(_camerasTransitionData[3].BlendTime);
            _sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[4]));
            _sequence.AppendInterval(_camerasTransitionData[4].BlendTime + 3f);
            _sequence.AppendCallback(() => EyeCurtainSequence());
            _sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[5]));
            _sequence.AppendInterval(3f);
            _sequence.AppendCallback(PassCutScene);
            _sequence.AppendCallback(SaveProgress);
            _sequence.AppendCallback(() => _stateMachine.Enter<LoadLevelState, string>(_transferTo));
        }

        private TweenCallback ParentEquipmentToMain(HeroCameraWrapper wrapper) 
            => wrapper.ParentEquipmentToMainCamera;

        private void SaveProgress()
        {
            _progressService.PlayerProgress.WorldData.PositionOnLevel.CurrentLevel = _transferTo;
           _saveLoadService.SaveProgress();
        }

        private Sequence EyeCurtainSequence()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(ToggleEyeCurtain(0.4f,2f));
            sequence.Append(ToggleEyeCurtain(0f,1f));
            sequence.Append(ToggleEyeCurtain(0.4f,1f));
            sequence.Append(ToggleEyeCurtain(0.5f,1f));
            sequence.Append(ToggleEyeCurtain(1f,2f));
            return sequence;
        }

        private Tween ToggleEyeCurtain(float value,float duration) 
            => _eyeCurtain.DOFade(value, duration);

        private async void SpawnEyeCurtain()
        {
            GameObject handle = await _uiFactory.CreateCurtain();
            _eyeCurtain = handle.GetComponent<CanvasGroup>();
        }

        private void ChangeCamera(CutSceneCameraTransitionData data)
        {
            _cameraService.Brain.m_DefaultBlend.m_CustomCurve = data.BlendCurve;
            _cameraService.Brain.m_DefaultBlend.m_Time = data.BlendTime;
            _cameraService.ChangeCamerasPriority(data.Type);
        }
    }
}