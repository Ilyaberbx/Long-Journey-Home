using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using Logic.Animations;
using Logic.Camera;
using Logic.Car;
using Logic.Player;
using UI.Services.Factory;
using UnityEngine;
using Zenject;

namespace Logic.CutScenes
{
    public class FinalCarCutScene : BaseCutScene
    {
        [SerializeField] private Transform _bearTarget;
        [SerializeField] private GameObject _bear;
        [SerializeField] private CarLights _lights;
        [SerializeField] private List<Transform> _bearCutScenePositions;
        [SerializeField] private List<CutSceneCameraTransitionData> _camerasTransitionData;
        private ICameraService _cameraService;
        private BearAnimator _bearAnimator;
        private IUIFactory _uiFactory;
        private CanvasGroup _finalCurtain;

        [Inject]
        public void Construct(ICameraService cameraService,IUIFactory uiFactory)
        {
            _cameraService = cameraService;
            _uiFactory = uiFactory;
        }

        private async void Start()
        {
            _bear.SetActive(false);
            _bearAnimator = _bear.GetComponent<BearAnimator>();

            if (IsCutScenePassed())
                DisableTriggers();
            else
                await LoadCurtain();
        }

        public override void StartCutScene(Transform player, Action onCutSceneEnded)
        {
            Sequence sequence = DOTween.Sequence();
            DisablePlayer(player);
            sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[0]));
            sequence.AppendInterval(_camerasTransitionData[0].BlendTime + 1.5f);
            sequence.Append(_lights.KickstartLights(0.5f, 0.8f));
            sequence.AppendInterval(0.8f);
            sequence.AppendCallback(() => BearRunningSequence(_bearTarget.position));
            sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[1]));
            sequence.AppendInterval(_camerasTransitionData[1].BlendTime +  0.8f);
            sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[0]));
            sequence.AppendInterval(_camerasTransitionData[0].BlendTime + 0.4f);
            sequence.Append(_lights.KickstartLights(0.8f, 0.5f));
            sequence.Append(_lights.KickstartLights(1f, 0.8f));
            sequence.Append(_lights.KickstartLights(1.4f, 2f));
            sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[2]));
            sequence.AppendInterval(0.6f);
            sequence.AppendCallback(AnimateBearAttack);
            sequence.AppendInterval(_camerasTransitionData[2].BlendTime);
            sequence.AppendCallback(ShowEndCurtain);
        }

        private void ShowEndCurtain() 
            => _finalCurtain.DOFade(1, 0f);

        private async Task LoadCurtain()
        {
           GameObject handle = await _uiFactory.CreateCurtain();
           _finalCurtain = handle.GetComponent<CanvasGroup>();
        }

        private void AnimateBearAttack()
        {
            _bear.transform.position = _bearCutScenePositions[0].position;
            _bear.GetComponent<Animator>().speed = 0.8f;
            _bearAnimator.PlayAttack(2);
        }

        private Tween BearRunningSequence(Vector3 target)
        {
            _bear.SetActive(true);
            _bearAnimator.Move(1);
            return _bear.transform.DOMove(target, 3).SetEase(Ease.Linear);
        }
        private void ChangeCamera(CutSceneCameraTransitionData data)
        {
            _cameraService.Brain.m_DefaultBlend.m_CustomCurve = data.BlendCurve;
            _cameraService.Brain.m_DefaultBlend.m_Time = data.BlendTime;
            _cameraService.ChangeCamerasPriority(data.Type);
        }

        private void DisablePlayer(Transform player) 
            => player.gameObject.SetActive(false);
    }
}