using System;
using System.Collections.Generic;
using DG.Tweening;
using Extensions;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.Dialogue;
using Logic.Camera;
using Logic.DialogueSystem;
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
        [SerializeField] private AssetReference _magicFxReference;
        [SerializeField] private GameObject _block;
        [SerializeField] private Transform _pillar;
        [SerializeField] private Transform _rabbit;
        [SerializeField] private Dialogue _afterSceneDialogue;
        private ICameraService _camerasService;
        private IAssetProvider _assetProvider;
        private GameObject _smokeFxPrefab;
        private GameObject _magicFxPrefab;
        private IDialogueService _dialogueService;

        [Inject]
        public void Construct(ICameraService camerasService, IAssetProvider assetProvider,IDialogueService dialogueService)
        {
            _camerasService = camerasService;
            _assetProvider = assetProvider;
            _dialogueService = dialogueService;
        }

        protected override async void OnAwake()
        {
            if (IsCutScenePassed())
            {
                DisableTriggers();
                return;
            }

            _smokeFxPrefab = await _assetProvider.Load<GameObject>(_smokeReference);
            _magicFxPrefab = await _assetProvider.Load<GameObject>(_magicFxReference);
        }

        public override void StartCutScene(Transform player, Action onCutSceneEnded)
        {
            _sequence = DOTween.Sequence();
            _sequence.AppendCallback(DisableTriggers);
            _sequence.AppendCallback(ParentEquipmentToMain(player.GetComponent<HeroCameraWrapper>()));
            _sequence.AppendCallback(() => ChangeCamera(_transitionDatas[0]));
            _sequence.AppendInterval(_transitionDatas[0].BlendTime);
            _sequence.AppendInterval(1f);
            _sequence.AppendCallback(() => ZoomCamera(50f));
            _sequence.AppendInterval(5f);
            _sequence.AppendCallback(() => ZoomCamera(66f));
            _sequence.AppendInterval(1f);
            _sequence.AppendCallback(PlayCracklingSound);
            _sequence.AppendInterval(1f);
            _sequence.AppendCallback(() => ChangeCamera(_transitionDatas[1]));
            _sequence.AppendCallback(() => ShowSmokeFx(_smokeSpawnPoints[0]));
            _sequence.AppendCallback(() => ShowSmokeFx(_smokeSpawnPoints[1]));
            _sequence.Append(FallPillar());
            _sequence.AppendCallback(DissolveRabbit);
            _sequence.AppendCallback(() => ShakeCamera(1, 20));
            _sequence.AppendCallback(() => ShowSmokeFx(_smokeSpawnPoints[2]));
            _sequence.AppendCallback(ActivateBlock);
            _sequence.AppendInterval(1f);
            _sequence.AppendCallback(() => ChangeCamera(_transitionDatas[2]));
            _sequence.AppendInterval(6f);
            _sequence.AppendCallback(PlayRoarSound);
            _sequence.AppendInterval(0.3f);
            _sequence.AppendCallback(() => ChangeCamera(_transitionDatas[3]));
            _sequence.AppendInterval(_transitionDatas[3].BlendTime + 1f);
            _sequence.AppendCallback(() => _dialogueService.StartDialogue(_afterSceneDialogue));
            _sequence.AppendInterval(1.5f);
            _sequence.AppendCallback(() => ChangeCamera(_transitionDatas[4]));
            _sequence.AppendCallback(() => onCutSceneEnded?.Invoke());
        }

        private void PlayCracklingSound()
        {
            
        }

        private void PlayRoarSound()
        {
            
        }

        private void DissolveRabbit()
        {
            _rabbit.gameObject.SetActive(false);
            ShowMagicFx();
        }

        private void ShowMagicFx() 
            => Instantiate(_magicFxPrefab, _rabbit.transform.position, Quaternion.identity);

        private void ShakeCamera(float duration, float strength)
            => _camerasService.CurrentGameCamera().Camera.transform.DOShakePosition(duration, strength);

        private void ZoomCamera(float zoomValue)
            => DOTween.To(() => _camerasService.CurrentGameCamera().Camera.m_Lens.FieldOfView,
                x => _camerasService.CurrentGameCamera().Camera.m_Lens.FieldOfView = x, zoomValue, 1f);

        private void ActivateBlock()
            => _block.SetActive(true);

        private Tween FallPillar()
            => _pillar.DOLocalRotate(_pillar.transform.localRotation.eulerAngles.AddX(45), 1.4f).SetEase(Ease.InExpo);

        private TweenCallback ParentEquipmentToMain(HeroCameraWrapper wrapper)
            => wrapper.ParentEquipmentToMainCamera;

        private void ShowSmokeFx(Transform at)
            => Instantiate(_smokeFxPrefab, at.position, Quaternion.identity);

        private void ChangeCamera(CutSceneCameraTransitionData data)
        {
            _camerasService.Brain.m_DefaultBlend.m_CustomCurve = data.BlendCurve;
            _camerasService.Brain.m_DefaultBlend.m_Time = data.BlendTime;
            _camerasService.ChangeCamerasPriority(data.Type);
        }
        
    }
}