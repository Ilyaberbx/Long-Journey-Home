using System;
using DG.Tweening;
using Extensions;
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
        private CanvasGroup _eyeCurtain;
        private ICameraService _cameraService;
        private IUIFactory _uiFactory;

        protected override void OnAwake()
        {
            if (IsCutScenePassed()) 
                DisableTriggers();
            else
                SpawnEyeCurtain();
        }

        [Inject]
        public void Construct(ICameraService cameraService,IUIFactory uiFactory)
        {
            _cameraService = cameraService;
            _uiFactory = uiFactory;
        }

        public override void StartCutScene(Transform player, Action onCutSceneEnded)
        {
            HideEquip(player);
            _sequence = DOTween.Sequence();
            _sequence.AppendCallback(DisableTriggers);
            _sequence.Append(OpenDoor());
            _sequence.AppendCallback(() => ChangeCamera(_transitionDatas[0]));
        }

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