using System;
using Infrastructure.Services.PersistentProgress;
using Logic.Spawners;
using UnityEngine;
using Zenject;

namespace Logic.CutScenes
{
    public abstract class BaseCutScene : BaseMarker,ICutScene
    {
        [SerializeField] private UniqueId _uniqueId;
        private IPersistentProgressService _progressService;

        [Inject]
        public void Construct(IPersistentProgressService progressService) 
            => _progressService = progressService;

        protected bool IsCutScenePassed() 
            => _progressService.PlayerProgress.CutSceneData.Passed.Contains(_uniqueId.Id);

        protected void PassCutScene()
            => _progressService.PlayerProgress.CutSceneData.Passed.Add(_uniqueId.Id);

        public abstract void StartCutScene(Transform player, Action onCutSceneEnded);
    }
}