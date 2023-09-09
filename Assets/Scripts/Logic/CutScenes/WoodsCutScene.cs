using System;
using DG.Tweening;
using Enums;
using Infrastructure.Services.AssetManagement;
using Logic.Camera;
using Logic.DialogueSystem;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Logic.CutScenes
{
    public class WoodsCutScene : BaseCutScene
    {
        [SerializeField] private AssetReference _smokeReference;
        [SerializeField] private Dialogue _dialogueOnEnd;
        [SerializeField] private Transform _firstTree;
        [SerializeField] private Transform _secondTree;
        [SerializeField] private Transform _firstSmokePosition;
        [SerializeField] private Transform _secondSmokePosition;
        [SerializeField] private float _firstTreeRotateValue;
        [SerializeField] private float _secondTreeRotateValue;
        private ICameraService _camerasService;
        private IAssetProvider _assetProvider;
        private GameObject _smokeFxPrefab;

        [Inject]
        public void Construct(ICameraService camerasService,IAssetProvider assetProvider)
        {
            _camerasService = camerasService;
            _assetProvider = assetProvider;
        }

        protected override async void OnAwake()
        {
            if (IsCutScenePassed())
            {
                DisableTriggers();
                return;
            }
            _smokeFxPrefab = await _assetProvider.Load<GameObject>(_smokeReference);
        }

        public override void StartCutScene(Transform player, Action onCutSceneEnded)
        {
            IDialogueActor dialogue = player.GetComponent<IDialogueActor>();
            _sequence = DOTween.Sequence();
            _sequence.AppendCallback(() => _camerasService.ChangeCamerasPriority(GameCameraType.FirstCamera));
            _sequence.Append(FallFirstTree());
            _sequence.AppendCallback(() => ShakeCamera(50f));
            _sequence.AppendCallback(() => ShowFx(_firstSmokePosition));
            _sequence.AppendCallback(() => _camerasService.ChangeCamerasPriority(GameCameraType.SecondCamera));
            _sequence.Append(FallSecondTree());
            _sequence.AppendCallback(() => ShakeCamera(10f));
            _sequence.AppendCallback(() => ShowFx(_secondSmokePosition));
            _sequence.AppendCallback(() => _camerasService.ChangeCamerasPriority(GameCameraType.PlayerCamera));
            _sequence.AppendInterval(1f);
            _sequence.AppendCallback(() => dialogue.StartDialogue(_dialogueOnEnd));
            _sequence.AppendCallback(PassCutScene);
            _sequence.AppendCallback(DisableTriggers);
            _sequence.AppendCallback(() => onCutSceneEnded?.Invoke());
        }
        

        private void ShakeCamera(float strength) 
            => _camerasService.CurrentGameCamera().Camera.transform.DOShakePosition(1f, strength);

        private void ShowFx(Transform at) 
            => Instantiate(_smokeFxPrefab, at.position, Quaternion.identity);


        private Tween FallSecondTree() 
            => _secondTree.DOLocalRotate(Vector3.left * _secondTreeRotateValue, 1.8f).SetEase(Ease.InExpo, 4);

        private Tween FallFirstTree() 
            => _firstTree.DORotate(Vector3.forward * _firstTreeRotateValue, 2f).SetEase(Ease.InExpo);
    }
}