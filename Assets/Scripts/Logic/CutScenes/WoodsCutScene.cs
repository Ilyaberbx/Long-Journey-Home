using System;
using DG.Tweening;
using Enums;
using Infrastructure.Services.AssetManagement;
using Logic.Camera;
using Logic.CutScenes.Sound;
using Logic.DialogueSystem;
using Logic.Traps;
using Sound.SoundSystem;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Logic.CutScenes
{
    public class WoodsCutScene : BaseCutScene
    {
        [SerializeField] private SoundOperations _soundOperations;
        [SerializeField] private AssetReference _smokeReference;
        [SerializeField] private Dialogue _dialogueOnEnd;
        [SerializeField] private Transform _firstTree;
        [SerializeField] private Transform _firstSmokePosition;
        [SerializeField] private float _firstTreeRotateValue;
        private IAssetProvider _assetProvider;
        private ICameraService _cameraService;
        private GameObject _smokeFxPrefab;

        [Inject]
        public void Construct(ICameraService cameraService,IAssetProvider assetProvider)
        {
            _cameraService = cameraService;
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
            _sequence.AppendCallback(() => onCutSceneEnded?.Invoke());
            _sequence.AppendCallback(_soundOperations.PlaySound<CrashingSoundOperator>);
            _sequence.Append(FallFirstTree());
            _sequence.AppendCallback(() => ShakeCamera(50f));
            _sequence.AppendCallback(() => ShowFx(_firstSmokePosition));
            _sequence.AppendInterval(1f);
            _sequence.AppendCallback(() => dialogue.StartDialogue(_dialogueOnEnd));
            _sequence.AppendCallback(PassCutScene);
            _sequence.AppendCallback(DisableTriggers);
        }
        

        private void ShakeCamera(float strength) 
            => _cameraService.CurrentGameCamera().Camera.transform.DOShakePosition(1f, strength);

        private void ShowFx(Transform at) 
            => Instantiate(_smokeFxPrefab, at.position, Quaternion.identity);

        private Tween FallFirstTree() 
            => _firstTree.DORotate(Vector3.forward * _firstTreeRotateValue, 2f).SetEase(Ease.InExpo);
        
    }
}