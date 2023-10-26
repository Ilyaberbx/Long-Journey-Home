using System;
using System.Collections.Generic;
using DG.Tweening;
using Extensions;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.MusicService;
using Infrastructure.Services.SaveLoad;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.State;
using Logic.Animations;
using Logic.Camera;
using Logic.CutScenes.Sound;
using Logic.DialogueSystem;
using Logic.Enemy.Sound;
using Logic.Inventory;
using Logic.Inventory.Item;
using Logic.Player;
using Sound.SoundSystem;
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
        [SerializeField] private SoundOperations _soundOperations;
        [SerializeField] private Transform _firstSmokeSpawnPoint;
        [SerializeField] private Transform _secondSmokeSpawnPoint;
        [SerializeField] private string _transferTo;
        private ICameraService _cameraService;
        private IAssetProvider _assetProvider;
        private GameObject _smokeFxPrefab;
        private ISaveLoadService _saveLoad;
        private IGameStateMachine _stateMachine;
        private EnemyAnimator _enemyAnimator;
        
        [Inject]
        public void Construct(ICameraService cameraService, IAssetProvider assetProvider,
            ISaveLoadService saveLoadService, IGameStateMachine stateMachine)
        {
            _cameraService = cameraService;
            _assetProvider = assetProvider;
            _saveLoad = saveLoadService;
            _stateMachine = stateMachine;
        }

        protected override async void OnAwake()
        {
            if (IsCutScenePassed())
                DisableTriggers();

            _smokeFxPrefab = await _assetProvider.Load<GameObject>(_smokeReference);
            _enemyAnimator = _bear.GetComponent<EnemyAnimator>();
        }

        public override void StartCutScene(Transform player, Action onCutSceneEnded)
        {
            if (!LadderIsPicked(player))
            {
                onCutSceneEnded?.Invoke();
                player.GetComponent<DialogueActor>().StartDialogue(_noLadderDialogue);
                return;
            }

            player.GetComponent<HeroEquiper>().ClearUp();
            CutSceneSequence(player);
        }

        private bool LadderIsPicked(Transform player)
            => HasInventory(player, out InventoryPresenter inventory) && TryWithdrawLadder(inventory);

        private void CutSceneSequence(Transform player)
        {
            HeroCameraWrapper cameraWrapper = player.GetComponent<HeroCameraWrapper>();
            _sequence = DOTween.Sequence();
            _sequence.AppendCallback(ParentEquipmentToMain(cameraWrapper));
            _sequence.AppendCallback(DisableTriggers);
            _sequence.AppendCallback(PassCutScene);
            _sequence.AppendCallback(DisableBridgeObstacle);
            _sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[0]));
            _sequence.AppendInterval(_camerasTransitionData[0].BlendTime + 0.4f);
            _sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[1]));
            _sequence.AppendInterval(_camerasTransitionData[1].BlendTime + 0.4f);
            _sequence.AppendCallback(EnableLadder);
            _sequence.AppendCallback(PlayLadderPutSound);
            _sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[0]));
            _sequence.AppendInterval(_camerasTransitionData[0].BlendTime + 0.4f);
            _sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[2]));
            _sequence.AppendCallback(() => BearRunningSequence(_bearFirstTarget.position));
            _sequence.AppendInterval(2f);
            _sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[3]));
            _sequence.AppendInterval(_camerasTransitionData[3].BlendTime);
            _sequence.AppendCallback(() => _bear.SetActive(false));
            _sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[4]));
            _sequence.AppendInterval(_camerasTransitionData[4].BlendTime + 0.3f);
            _sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[5]));
            _sequence.AppendInterval(_camerasTransitionData[5].BlendTime + 0.4f);
            _sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[6]));
            _sequence.AppendInterval(_camerasTransitionData[6].BlendTime + 0.2f);
            _sequence.AppendCallback(() => _bear.transform.position = _bearSecondPosition.position);
            _sequence.AppendCallback(() => BearRunningSequence(_bearSecondTarget.position));
            _sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[7]));
            _sequence.AppendInterval(2f);
            _sequence.AppendCallback(PlayCrashingSound);
            _sequence.AppendCallback(PlayRoarSound);
            _sequence.AppendCallback(FallPlank);
            _sequence.AppendCallback(AnimateBearAttack);
            _sequence.AppendInterval(0.1f);
            _sequence.AppendCallback(() => SpawnSmoke(_firstSmokeSpawnPoint.position));
            _sequence.AppendCallback(FallLadder);
            _sequence.AppendInterval(0.3f);
            _sequence.AppendCallback(FallBear);
            _sequence.AppendInterval(0.4f);
            _sequence.AppendCallback(() => SpawnSmoke(_secondSmokeSpawnPoint.position));
            _sequence.AppendInterval(1f);
            _sequence.AppendCallback(() => _enemyAnimator.PlayDeath());
            _sequence.AppendInterval(1f);
            _sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[6]));
            _sequence.AppendInterval(_camerasTransitionData[6].BlendTime);
            _sequence.AppendCallback(() => ChangeCamera(_camerasTransitionData[8]));
            _sequence.AppendInterval(0.7f);
            _sequence.AppendCallback(SaveProgress);
            _sequence.AppendCallback(() => _stateMachine.Enter<LoadLevelState, string, AmbienceType>(_transferTo, AmbienceType.ForestAmbience));
        }

        private void PlayLadderPutSound() 
            => _soundOperations.PlaySound<PutLadderOperator>();

        private void PlayCrashingSound() 
            => _soundOperations.PlaySound<CrashingSoundOperator>();
        
        private void PlayRoarSound() 
            => _soundOperations.PlaySound<RoarOperator>();

        private void AnimateBearAttack()
        {
            _bear.GetComponent<Animator>().speed = 0.3f;
            _enemyAnimator.PlayAttack(1);
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
            _progressService.Progress.WorldData.PositionOnLevel.CurrentLevel = _transferTo;
            _progressService.Progress.AmbienceProgress.CurrentAmbience = AmbienceType.ForestAmbience;
            
            _saveLoad.SavePlayerProgress();
        }

        private Tween BearRunningSequence(Vector3 target)
        {
            _bear.SetActive(true);
            _enemyAnimator.Move(1);
            return _bear.transform.DOMove(target, 3f).SetEase(Ease.Linear);
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