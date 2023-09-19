using System;
using Data;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;
using Zenject;

namespace UI.Elements
{
    public abstract class WindowBase : MonoBehaviour
    {
        private Action _close;
        protected PlayerProgress _progress => _progressService.PlayerProgress;
        private IPersistentProgressService _progressService;

        [Inject]
        public void Construct(IPersistentProgressService progressService)
            => _progressService = progressService;

        private void Awake()
            => OnAwake();

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        private void OnDestroy()
            => CleanUp();

        protected virtual void OnAwake()
        {
        }

        public void SubscribeCloseListener(Action onClose)
            => _close = onClose;

        public void Close()
        {
            _close?.Invoke();
            Destroy(gameObject);
        }

        protected virtual void Initialize()
        {
        }

        protected virtual void SubscribeUpdates()
        {

        }

        protected virtual void CleanUp()
        {
        }
    }
}