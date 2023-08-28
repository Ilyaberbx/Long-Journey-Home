using System;
using System.Collections.Generic;
using DG.Tweening;
using Extensions;
using Infrastructure.Services.AssetManagement;
using Logic.Camera;
using Logic.Player;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Logic.CutScenes
{
    public class WolfAssaultCutScene : BaseCutScene
    {
        [SerializeField] private List<CutSceneCameraTransitionData> _transitionDatas;
        [SerializeField] private List<Transform> _smokeSpawnPoints;
        [SerializeField] private AssetReference _smokeReference;
        [SerializeField] private GameObject _block;
        [SerializeField] private Transform _pillar;
        [SerializeField] private Transform _rabbit;
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
        public void Construct(ICameraService camerasService, IAssetProvider assetProvider)
        {
            _camerasService = camerasService;
            _assetProvider = assetProvider;
        }

        public override void StartCutScene(Transform player, Action onCutSceneEnded)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.AppendCallback(DisableTriggers);
            sequence.AppendCallback(() => ChangeCamera(_transitionDatas[0]));
            sequence.AppendInterval(1f);
            sequence.AppendCallback(ParentEquipmentToMain(player.GetComponent<HeroCameraWrapper>()));
            sequence.AppendInterval(_transitionDatas[0].BlendTime);
            sequence.AppendCallback(() => ZoomCamera(45f));
            sequence.AppendInterval(5f);
            sequence.AppendCallback(() => ZoomCamera(66));
            sequence.AppendCallback(() => ChangeCamera(_transitionDatas[1]));
            sequence.AppendCallback(() => ShowFx(_smokeSpawnPoints[0]));
            sequence.AppendCallback(() => ShowFx(_smokeSpawnPoints[1]));
            sequence.Append(FallPillar());
            sequence.AppendCallback(DisableRabbit);
            sequence.AppendCallback(() => ShakeCamera(1, 40));
            sequence.AppendCallback(() => ShowFx(_smokeSpawnPoints[2]));
            sequence.AppendCallback(PlayWolfRoar);
            sequence.AppendCallback(ActivateBlock);
            sequence.AppendInterval(1f);
            sequence.AppendCallback(() => ChangeCamera(_transitionDatas[0]));
            sequence.AppendInterval(_transitionDatas[0].BlendTime);
            sequence.AppendCallback(() => onCutSceneEnded?.Invoke());
        }

        private void DisableRabbit() 
            => _rabbit.gameObject.SetActive(false);
        
        private void ShakeCamera(float duration, float strength)
            => _camerasService.CurrentGameCamera().Camera.transform.DOShakePosition(duration, strength);

        private void ZoomCamera(float zoomValue) 
            => DOTween.To(() => _camerasService.CurrentGameCamera().Camera.m_Lens.FieldOfView, x => _camerasService.CurrentGameCamera().Camera.m_Lens.FieldOfView = x, zoomValue, 1f);

        private void ActivateBlock()
            => _block.SetActive(true);

        private Tween FallPillar()
            => _pillar.DOLocalRotate(_pillar.transform.localRotation.eulerAngles.AddX(45), 1.4f).SetEase(Ease.InExpo);
        
        private TweenCallback ParentEquipmentToMain(HeroCameraWrapper wrapper) 
            => wrapper.ParentEquipmentToMainCamera;

        private void ShowFx(Transform at)
            => Instantiate(_smokeFxPrefab, at.position, Quaternion.identity);

        private void ChangeCamera(CutSceneCameraTransitionData data)
        {
            _camerasService.Brain.m_DefaultBlend.m_CustomCurve = data.BlendCurve;
            _camerasService.Brain.m_DefaultBlend.m_Time = data.BlendTime;
            _camerasService.ChangeCamerasPriority(data.Type);
        }

        private void PlayWolfRoar()
        {
        }
    }
}