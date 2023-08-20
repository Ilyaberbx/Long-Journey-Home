using System;
using System.Collections.Generic;
using DG.Tweening;
using Extensions;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.SaveLoad;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.State;
using Logic.Animations;
using Logic.Camera;
using Logic.DialogueSystem;
using Logic.Inventory;
using Logic.Inventory.Item;
using Logic.Player;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Logic.CutScenes
{
    public class BridgeCutScene : BaseCutScene
    {
        [SerializeField] private List<CutSceneCameraTransitionData> _camerasTransitionData;
        [SerializeField] private Transform _bearFirstTarget;
        [SerializeField] private Transform _bearSecondTarget;
        [SerializeField] private Transform _bearSecondPosition;
        [SerializeField] private GameObject _bear;
        [SerializeField] private GameObject _ladder;
        [SerializeField] private GameObject _plank;
        [SerializeField] private GameObject _bridgeObstacle;
        [SerializeField] private Dialogue _noLadderDialogue;
        [SerializeField] private ItemData _ladderItem;
        [SerializeField] private AssetReference _smokeReference;
        [SerializeField] private Transform _firstSmokeSpawnPoint;
        [SerializeField] private Transform _secondSmokeSpawnPoint;
        [SerializeField] private string _transferTo;
        private ICameraService _cameraService;
        private IAssetProvider _assetProvider;
        private GameObject _smokeFxPrefab;
        private ISaveLoadService _saveLoad;
        private IGameStateMachine _stateMachine;
        private BearAnimator _bearAnimator;
        
        [Inject]
        public void Construct(ICameraService cameraService, IAssetProvider assetProvider,
            ISaveLoadService saveLoadService, IGameStateMachine stateMachine)
        {
            _cameraService = cameraService;
            _assetProvider = assetProvider;
            _saveLoad = saveLoadService;
            _stateMachine = stateMachine;
        }

        private async void Start()
        {
            if (IsCutScenePassed())
                DisableTriggers();

            _smokeFxPrefab = await _assetProvider.Load<GameObject>(_smokeReference);
            _bearAnimator = _bear.GetComponent<BearAnimator>();
        }

        public override void StartCutScene(Transform player, Action onCutSceneEnded)
        {
            if (!LadderIsPicked(player))
            {
                onCutSceneEnded?.Invoke();
                player.GetComponent<DialogueActor>().StartDialogue(_noLadderDialogue);
                return;
            }

            CutSceneSequence(player);
        }

        private bool LadderIsPicked(Transform player)
            => HasInventory(player, out InventoryPresenter inventory) && TryWithdrawLadder(inventory);

        private void CutSceneSequence(Transform player)
        {
            Sequence sequence = DOTween.Sequence();
            HeroCameraWrapper cameraWrapper = player.GetComponent<HeroCameraWrapper>();
            sequence.AppendCallback(ParentEquipmentToMain(cameraWrapper));
            sequence.AppendCallback(DisableTriggers);
            sequence.AppendCallback(PassCutScene);
            sequence.AppendCallback(DisableBridgeObstacle);
            sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[0]));
            sequence.AppendInterval(_camerasTransitionData[0].BlendTime + 0.4f);
            sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[1]));
            sequence.AppendInterval(_camerasTransitionData[1].BlendTime + 0.4f);
            sequence.AppendCallback(EnableLadder);
            sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[0]));
            sequence.AppendInterval(_camerasTransitionData[0].BlendTime + 0.4f);
            sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[2]));
            sequence.AppendCallback(() => BearRunningSequence(_bearFirstTarget.position));
            sequence.AppendInterval(2f);
            sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[3]));
            sequence.AppendInterval(_camerasTransitionData[3].BlendTime);
            sequence.AppendCallback(() => _bear.SetActive(false));
            sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[4]));
            sequence.AppendInterval(_camerasTransitionData[4].BlendTime + 0.3f);
            sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[5]));
            sequence.AppendInterval(_camerasTransitionData[5].BlendTime + 0.4f);
            sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[6]));
            sequence.AppendInterval(_camerasTransitionData[6].BlendTime + 0.2f);
            sequence.AppendCallback(() => _bear.transform.position = _bearSecondPosition.position);
            sequence.AppendCallback(() => BearRunningSequence(_bearSecondTarget.position));
            sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[7]));
            sequence.AppendInterval(2f);
            sequence.AppendCallback(FallPlank);
            sequence.AppendCallback(AnimateBearAttack);
            sequence.AppendInterval(0.1f);
            sequence.AppendCallback(() => SpawnSmoke(_firstSmokeSpawnPoint.position));
            sequence.AppendCallback(FallLadder);
            sequence.AppendInterval(0.3f);
            sequence.AppendCallback(FallBear);
            sequence.AppendInterval(0.4f);
            sequence.AppendCallback(() => SpawnSmoke(_secondSmokeSpawnPoint.position));
            sequence.AppendInterval(1f);
            sequence.AppendCallback(() => _bearAnimator.PlayDeath());
            sequence.AppendInterval(1f);
            sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[6]));
            sequence.AppendInterval(_camerasTransitionData[6].BlendTime);
            sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[8]));
            sequence.AppendInterval(0.7f);
            sequence.AppendCallback(SaveProgress);
            sequence.AppendCallback(() => _stateMachine.Enter<LoadLevelState, string>(_transferTo));
        }

        private void AnimateBearAttack()
        {
            _bear.GetComponent<Animator>().speed = 0.3f;
            _bearAnimator.PlayAttack(1);
        }

        private void SpawnSmoke(Vector3 position)
            => Instantiate(_smokeFxPrefab, position, Quaternion.identity);

        private void FallBear()
            => _bear.transform.DOMove(_bear.transform.position.AddY(-60), 0.7f).SetEase(Ease.InExpo);

        private void FallPlank()
            => _plank.transform.DORotate(Vector3.zero.AddX(-65f), 0.7f).SetEase(Ease.InExpo);

        private void FallLadder()
            => _ladder.transform.DORotate(Vector3.zero.AddZ(-35f), 0.8f).SetEase(Ease.InExpo);

        private void SaveProgress()
        {
            _progressService.PlayerProgress.WorldData.PositionOnLevel.CurrentLevel = _transferTo;
            _saveLoad.SaveProgress();
        }

        private Tween BearRunningSequence(Vector3 target)
        {
            _bear.SetActive(true);
            _bearAnimator.Move(1);
            return _bear.transform.DOMove(target, 2.5f).SetEase(Ease.Linear);
        }

        private void EnableLadder()
            => _ladder.SetActive(true);

        private void ChangeCamera(CutSceneCameraTransitionData data)
        {
            _cameraService.Brain.m_DefaultBlend.m_CustomCurve = data.BlendCurve;
            _cameraService.Brain.m_DefaultBlend.m_Time = data.BlendTime;
            _cameraService.ChangeCamerasPriority(data.Type);
        }

        private bool TryWithdrawLadder(InventoryPresenter inventory)
            => inventory.TryRemoveItemById(_ladderItem.Id, 1);

        private bool HasInventory(Transform player, out InventoryPresenter inventory)
            => player.TryGetComponent(out inventory);

        private void DisableBridgeObstacle()
            => _bridgeObstacle.SetActive(false);

        private TweenCallback ParentEquipmentToMain(HeroCameraWrapper wrapper)
            => wrapper.ParentEquipmentToMainCamera;
    }
}