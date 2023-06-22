using System;
using System.Collections.Generic;
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
    public class IntroCutScene : BaseCutScene
    {
        [SerializeField] private List<Collider> _cutSceneTriggers;
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

        private async void Awake()
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
            Sequence sequence = DOTween.Sequence();
            sequence.AppendCallback(() => _camerasService.ChangeCamerasPriority(GameCameraType.FirstCamera));
            sequence.Append(FallFirstTree());
            sequence.AppendCallback(() => ShakeCamera(50f));
            sequence.AppendCallback(() => ShowFx(_firstSmokePosition));
            sequence.AppendCallback(() => _camerasService.ChangeCamerasPriority(GameCameraType.SecondCamera));
            sequence.Append(FallSecondTree());
            sequence.AppendCallback(() => ShakeCamera(10f));
            sequence.AppendCallback(() => ShowFx(_secondSmokePosition));
            sequence.AppendCallback(() => _camerasService.ChangeCamerasPriority(GameCameraType.PlayerCamera));
            sequence.AppendInterval(1f);
            sequence.AppendCallback(() => dialogue.StartDialogue(_dialogueOnEnd));
            sequence.AppendCallback(PassCutScene);
            sequence.AppendCallback(DisableTriggers);
            sequence.AppendCallback(() => onCutSceneEnded?.Invoke());
        }

        private void DisableTriggers()
        {
            foreach (Collider trigger in _cutSceneTriggers)
                trigger.enabled = false;
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