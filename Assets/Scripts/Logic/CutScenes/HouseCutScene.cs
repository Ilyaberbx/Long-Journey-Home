using System;
using System.Collections.Generic;
using DG.Tweening;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.State;
using Logic.Camera;
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

        [Inject]
        public void Construct(ICameraService cameraService,IUIFactory uiFactory,IGameStateMachine stateMachine)
        {
            _cameraService = cameraService;
            _uiFactory = uiFactory;
            _stateMachine = stateMachine;
        }

        private void Start()
        {
            if (IsCutScenePassed()) 
                DisableTriggers();
            else
                SpawnEyeCurtain();
        }

        public override void StartCutScene(Transform player, Action onCutSceneEnded)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.AppendCallback(DisableTriggers);
            sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[0]));
            sequence.AppendInterval(_camerasTransitionData[0].BlendTime + 1f);
            sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[1]));
            sequence.AppendInterval(_camerasTransitionData[1].BlendTime + 2f);
            sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[2]));
            sequence.AppendInterval(_camerasTransitionData[2].BlendTime);
            sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[3]));
            sequence.AppendInterval(_camerasTransitionData[3].BlendTime + 0.7f);
            sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[4]));
            sequence.AppendInterval(_camerasTransitionData[4].BlendTime + 1f);
            sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[5]));
            sequence.AppendCallback(() => EyeCurtainSequence());
            sequence.AppendInterval(_camerasTransitionData[5].BlendTime);
            sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[6]));
            sequence.AppendInterval(1f);
            sequence.AppendCallback(() => _stateMachine.Enter<LoadLevelState, string>(_transferTo));
            sequence.AppendCallback(() => onCutSceneEnded?.Invoke());
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
            GameObject handle = await _uiFactory.CreateEyeCurtain();
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