using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using Logic.Animations;
using Logic.Camera;
using Logic.Car;
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
        private EnemyAnimator _enemyAnimator;
        private IUIFactory _uiFactory;
        private CanvasGroup _finalCurtain;

        [Inject]
        public void Construct(ICameraService cameraService, IUIFactory uiFactory)
        {
            _cameraService = cameraService;
            _uiFactory = uiFactory;
        }

        protected override async void OnAwake()
        {
            _bear.SetActive(false);
            _enemyAnimator = _bear.GetComponent<EnemyAnimator>();

            if (IsCutScenePassed())
                DisableTriggers();
            else
                await LoadCurtain();
        }

        public override void StartCutScene(Transform player, Action onCutSceneEnded)
        {
            DisablePlayer(player);
            _sequence = DOTween.Sequence();
            _sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[0]));
            _sequence.AppendInterval(_camerasTransitionData[0].BlendTime + 1.5f);
            _sequence.Append(_lights.KickstartLights(0.5f, 0.8f));
            _sequence.AppendInterval(0.8f);
            _sequence.AppendCallback(() => BearRunningSequence(_bearTarget.position));
            _sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[1]));
            _sequence.AppendInterval(_camerasTransitionData[1].BlendTime + 0.8f);
            _sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[0]));
            _sequence.AppendInterval(_camerasTransitionData[0].BlendTime + 0.4f);
            _sequence.Append(_lights.KickstartLights(0.8f, 0.5f));
            _sequence.Append(_lights.KickstartLights(1f, 0.8f));
            _sequence.Append(_lights.KickstartLights(1.4f, 2f));
            _sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[2]));
            _sequence.AppendInterval(0.6f);
            _sequence.AppendCallback(AnimateBearAttack);
            _sequence.AppendInterval(_camerasTransitionData[2].BlendTime);
            _sequence.AppendCallback(ShowEndCurtain);
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
            _enemyAnimator.PlayAttack(2);
        }

        private Tween BearRunningSequence(Vector3 target)
        {
            _bear.SetActive(true);
            _enemyAnimator.Move(1);
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