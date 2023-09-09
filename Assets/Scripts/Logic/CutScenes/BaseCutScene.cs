using System;
using System.Collections.Generic;
using DG.Tweening;
using Infrastructure.Services.Pause;
using Infrastructure.Services.PersistentProgress;
using Logic.Spawners;
using UnityEngine;
using Zenject;

namespace Logic.CutScenes
{
    public abstract class BaseCutScene : BaseMarker, ICutScene, IPauseHandler
    {
        [SerializeField] private UniqueId _uniqueId;
        [SerializeField] protected List<Collider> _cutSceneTriggers;
        protected IPersistentProgressService _progressService;
        protected Sequence _sequence;
        private IPauseService _pauseService;

        [Inject]
        public void Construct(IPersistentProgressService progressService, IPauseService pauseService)
        {
            _progressService = progressService;
            _pauseService = pauseService;
        }

        private void Awake()
        {
            _sequence = DOTween.Sequence();
            OnAwake();
            Subscribe();
        }

        private void OnDestroy()
            => Unsubscribe();

        protected abstract void OnAwake();

        private void Subscribe()
            => _pauseService.Register(this);

        private void Unsubscribe()
            => _pauseService.UnRegister(this);

        protected bool IsCutScenePassed()
            => _progressService.PlayerProgress.CutSceneData.Passed.Contains(_uniqueId.Id);

        protected void PassCutScene()
            => _progressService.PlayerProgress.CutSceneData.Passed.Add(_uniqueId.Id);

        public abstract void StartCutScene(Transform player, Action onCutSceneEnded);

        protected void DisableTriggers()
        {
            foreach (Collider trigger in _cutSceneTriggers)
                trigger.enabled = false;
        }

        public virtual void HandlePause(bool isPaused)
            => _sequence.timeScale = isPaused ? 0 : 1;
    }
}