using System;
using System.Collections.Generic;
using DG.Tweening;
using Enums;
using Extensions;
using Infrastructure.Services.AssetManagement;
using Logic.Camera;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Logic.CutScenes
{

    public class WolfAssaultCutScene : BaseCutScene
    {
        [SerializeField] private List<Transform> _smokeSpawnPoints;
        [SerializeField] private AssetReference _smokeReference;
        [SerializeField] private GameObject _block;
        [SerializeField] private Transform _pillar;
        [SerializeField] private List<Collider> _cutSceneTriggers;
        private ICameraService _camerasService;
        private IAssetProvider _assetProvider;
        private GameObject _smokeFxPrefab;

        private async void Awake()
        {
            if (IsCutScenePassed())
            {
                DisableTriggers();
                return;
            }
            
            _smokeFxPrefab = await _assetProvider.Load<GameObject>(_smokeReference);
        }

        [Inject]
        public void Construct(ICameraService camerasService,IAssetProvider assetProvider)
        {
            _camerasService = camerasService;
            _assetProvider = assetProvider;
        }

        public override void StartCutScene(Transform player, Action onCutSceneEnded)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.AppendCallback(DisableTriggers);
            sequence.AppendCallback(SetFirstCamera);
            sequence.AppendCallback(() => ShowFx(_smokeSpawnPoints[0]));
            sequence.AppendCallback(() => ShowFx(_smokeSpawnPoints[1]));
            sequence.Append(FallPillar());
            sequence.AppendCallback(() => ShakeCamera(1,40));
            sequence.AppendCallback(() => ShowFx(_smokeSpawnPoints[2]));
            sequence.AppendCallback(PlayWolfRoar);
            sequence.AppendCallback(ActivateBlock);
            sequence.AppendInterval(1f);
            sequence.AppendCallback(SetPlayerCamera);
            sequence.AppendCallback(() => onCutSceneEnded?.Invoke());
        }
        

        private void DisableTriggers()
        {
            foreach (Collider trigger in _cutSceneTriggers)
                trigger.enabled = false;
        }
        private void ShakeCamera(float duration, float strength) 
            => _camerasService.CurrentGameCamera().Camera.transform.DOShakePosition(duration, strength);

        private void SetPlayerCamera() 
            => SetCamera(GameCameraType.PlayerCamera);

        private void ActivateBlock() 
            => _block.SetActive(true);

        private Tween FallPillar() 
            => _pillar.DOLocalRotate(_pillar.transform.localRotation.eulerAngles.AddX(45), 1.4f).SetEase(Ease.InExpo);
        
        private void ShowFx(Transform at) 
            => Instantiate(_smokeFxPrefab, at.position, Quaternion.identity);
        private void SetFirstCamera() 
            => SetCamera(GameCameraType.FirstCamera);

        private void SetCamera(GameCameraType type)
            => _camerasService.ChangeCamerasPriority(type);

        private void PlayWolfRoar()
        {
        }
    }
}