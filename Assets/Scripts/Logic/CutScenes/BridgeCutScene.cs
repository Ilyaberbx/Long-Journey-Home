using System;
using System.Collections.Generic;
using DG.Tweening;
using Logic.Camera;
using Logic.DialogueSystem;
using Logic.Inventory;
using Logic.Inventory.Item;
using Logic.Player;
using Logic.Spawners;
using UnityEngine;
using Zenject;

namespace Logic.CutScenes
{
    public class BridgeCutScene : BaseCutScene
    {
        [SerializeField] private EnemySpawnPoint _spawner;
        [SerializeField] private List<CutSceneCameraTransitionData> _camerasTransitionData;
        [SerializeField] private GameObject _ladder;
        [SerializeField] private GameObject _bridgeObstacle;
        [SerializeField] private Dialogue _noLadderDialogue;
        [SerializeField] private ItemData _ladderItem;
        private ICameraService _cameraService;

        [Inject]
        public void Construct(ICameraService cameraService)
            => _cameraService = cameraService;

        private void Start()
        {
            if (IsCutScenePassed())
                DisableTriggers();
        }

        public override void StartCutScene(Transform player, Action onCutSceneEnded)
        {
            if (!LadderIsPicked(player))
            {
                onCutSceneEnded?.Invoke();
                player.GetComponent<DialogueActor>().StartDialogue(_noLadderDialogue);
                return;
            }

            CutSceneSequence(player, onCutSceneEnded);
        }

        private bool LadderIsPicked(Transform player)
            => HasInventory(player, out InventoryPresenter inventory) && TryWithdrawLadder(inventory);

        private void CutSceneSequence(Transform player, Action onCutSceneEnded)
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
            sequence.AppendInterval(_camerasTransitionData[2].BlendTime);
            sequence.AppendCallback(() => onCutSceneEnded?.Invoke());
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